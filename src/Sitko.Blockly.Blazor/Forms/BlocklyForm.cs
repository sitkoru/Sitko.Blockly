using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly.Blazor.Forms
{
    using Helpers;
    using JetBrains.Annotations;
    using Sitko.Blazor.ScriptInjector;

    public interface IBlocklyForm
    {
        void ValidateBlocks();
    }

    public abstract class BlocklyForm<TEntity, TForm, TOptions> : InputBase<List<ContentBlock>>, IBlocklyForm
        where TForm : BaseForm<TEntity>
        where TEntity : class, new()
        where TOptions : BlazorBlocklyFormOptions, new()
    {
        private ContentBlock? blockToScroll;
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [Parameter]
        public TForm Form { get; set; } = null!;

        protected Dictionary<Guid, BlockForm> BlockForms { get; } = new();

        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;

        [PublicAPI]
        public IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;
        [Inject] protected BlocklyFormService BlocklyFormService { get; set; } = null!;
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

        private OrderedCollection<ContentBlock> OrderedBlocks { get; } = new();
        protected List<ContentBlock> Blocks { get; } = new();
        protected Dictionary<Guid, ElementReference> BlockElements { get; } = new();

        private TOptions FormOptions { get; set; } = new();

        [Parameter]
        public TOptions? Options
        {
            get => FormOptions;
            set
            {
                if (value is not null)
                {
                    FormOptions = value;
                }
            }
        }

        public void ValidateBlocks()
        {
            if (CurrentValue is not null)
            {
                foreach (var contentBlock in CurrentValue)
                {
                    ValidateBlock(contentBlock);
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            BlockDescriptors = Blockly.Descriptors
                .Where(d => FormOptions.AllowedBlocks.Any() == false || FormOptions.AllowedBlocks.Contains(d.Type))
                .OrderBy(d => FormOptions.BlockPriority(d)).ThenBy(d => d.Type.FullName).ToArray();
            OrderedBlocks.SetItems(CurrentValue?.OrderBy(b => b.Position) ?? new List<ContentBlock>().AsEnumerable());
            Blocks.AddRange(CurrentValue ?? new List<ContentBlock>());
            BlocklyFormService.AddForm(this);
        }

        protected bool CanAdd()
        {
            if (FormOptions.MaxBlocks > 0)
            {
                return CurrentValue?.Count < FormOptions.MaxBlocks;
            }

            return true;
        }

        protected bool CanAdd(IBlazorBlockDescriptor blockDescriptor)
        {
            if (!CanAdd())
            {
                return false;
            }

            var blockMaxCount = FormOptions.MaxBlockCount(blockDescriptor);
            if (blockMaxCount > 0)
            {
                var blocksCount = CurrentValue?.Count(b => b.GetType() == blockDescriptor.Type);
                if (blocksCount >= blockMaxCount)
                {
                    return false;
                }
            }

            return true;
        }

        private Task SaveBlockPositionAsync(ContentBlock block) =>
            JsRuntime.InvokeVoidAsync("Blockly.Forms.savePosition", BlockElements[block.Id]).AsTask();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await ScriptInjector.InjectAsync(JsHelper.FormsScriptRequest);
            }

            if (blockToScroll is not null)
            {
                var element = BlockElements[blockToScroll.Id];
                blockToScroll = null;

                await JsRuntime.InvokeVoidAsync("Blockly.Forms.scroll", element).AsTask();
            }
        }

        private async Task ScrollToBlockAsync(ContentBlock block)
        {
            await SaveBlockPositionAsync(block);
            blockToScroll = block;
        }

        protected void AddBlock(IBlazorBlockDescriptor blockDescriptor, ContentBlock? neighbor = null,
            bool after = true)
        {
            if (CanAdd(blockDescriptor))
            {
                var block = Blockly.CreateBlock(blockDescriptor);
                OrderedBlocks.AddItem(block, neighbor, after);
                Blocks.Add(block);
                UpdateForm();
                ValidateBlock(block);
            }
        }

        private void ValidateBlock(ContentBlock block)
        {
            foreach (var property in block.GetType().GetProperties())
            {
                Form.NotifyChange(new FieldIdentifier(block, property.Name));
            }
        }

        protected bool CanMoveBlockUp(ContentBlock block) =>
            block.Enabled && FormOptions.CanMoveBlock(block) && OrderedBlocks.CanMoveUp(block);

        protected bool CanMoveBlockDown(ContentBlock block) =>
            block.Enabled && FormOptions.CanMoveBlock(block) && OrderedBlocks.CanMoveDown(block);

        protected bool CanDeleteBlock(ContentBlock block) => block.Enabled && FormOptions.CanDeleteBlock(block);

        protected Task MoveBlockUpAsync(ContentBlock block)
        {
            OrderedBlocks.MoveUp(block);
            UpdateForm();
            return ScrollToBlockAsync(block);
        }


        protected Task MoveBlockDownAsync(ContentBlock block)
        {
            OrderedBlocks.MoveDown(block);
            UpdateForm();
            return ScrollToBlockAsync(block);
        }

        protected async Task DeleteBlockAsync(ContentBlock block)
        {
            var blockForm = BlockForms[block.Id];
            await blockForm.OnDeleteAsync();
            OrderedBlocks.RemoveItem(block);
            Blocks.Remove(block);
            UpdateForm();
            EditContext.Validate();
            BlocklyFormService.Validate();
        }

        private void UpdateForm() => CurrentValue = new List<ContentBlock>(OrderedBlocks.ToList());

        protected RenderFragment RenderBlockForm(IBlazorBlockDescriptor blockDescriptor, ContentBlock block) =>
            builder =>
            {
                builder.OpenComponent(0, blockDescriptor.FormComponent);
                builder.AddAttribute(1, "FormOptions", FormOptions);
                builder.AddAttribute(2, "Block", block);
                builder.AddComponentReferenceCapture(3, reference =>
                {
                    BlockForms[block.Id] = (BlockForm)reference;
                });
                builder.SetKey(block.Id);
                builder.CloseComponent();
            };

        protected override bool TryParseValueFromString(string? value, out List<ContentBlock> result,
            out string validationErrorMessage)
        {
            result = default!;
            validationErrorMessage = "";
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            BlocklyFormService.RemoveForm(this);
        }
    }
}
