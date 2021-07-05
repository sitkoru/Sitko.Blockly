using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly.Blazor.Forms
{
    public interface IBlocklyForm
    {
        void ValidateBlocks();
    }

    public abstract class BlocklyForm<TEntity, TForm, TOptions> : InputBase<List<ContentBlock>>, IBlocklyForm
        where TForm : BaseForm<TEntity>
        where TEntity : class
        where TOptions : BlazorBlocklyFormOptions, new()
    {
        [Parameter] public TForm Form { get; set; } = null!;
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;

        public IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;
        [Inject] protected BlocklyFormService BlocklyFormService { get; set; } = null!;

        protected readonly OrderedCollection<ContentBlock> OrderedBlocks = new();
        protected readonly List<ContentBlock> Blocks = new();

        protected TOptions FormOptions = new();

        [Parameter]
        public TOptions? Options
        {
            get
            {
                return FormOptions;
            }
            set
            {
                if (value is not null)
                {
                    FormOptions = value;
                }
            }
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            BlockDescriptors = Blockly.Descriptors
                .Where(d => FormOptions.AllowedBlocks.Any() == false || FormOptions.AllowedBlocks.Contains(d.Type))
                .OrderBy(d => FormOptions.BlockPriority(d) ?? d.Priority).ThenBy(d => d.Type.FullName).ToArray();
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

            int blockMaxCount = FormOptions.MaxBlockCount(blockDescriptor) ?? blockDescriptor.MaxCount;
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

        private void ValidateBlock(ContentBlock block)
        {
            foreach (var property in block.GetType().GetProperties())
            {
                Form.NotifyChange(new FieldIdentifier(block, property.Name));
            }
        }

        protected bool CanMoveBlockUp(ContentBlock block)
        {
            return OrderedBlocks.CanMoveUp(block);
        }

        protected bool CanMoveBlockDown(ContentBlock block)
        {
            return OrderedBlocks.CanMoveDown(block);
        }

        protected void MoveBlockUp(ContentBlock block)
        {
            OrderedBlocks.MoveUp(block);
            UpdateForm();
        }


        protected void MoveBlockDown(ContentBlock block)
        {
            OrderedBlocks.MoveDown(block);
            UpdateForm();
        }

        protected void DeleteBlock(ContentBlock block)
        {
            OrderedBlocks.RemoveItem(block);
            Blocks.Remove(block);
            UpdateForm();
            EditContext.Validate();
            BlocklyFormService.Validate();
        }

        private void UpdateForm()
        {
            CurrentValue = new List<ContentBlock>(OrderedBlocks.ToList());
        }

        protected RenderFragment RenderBlockForm(IBlazorBlockDescriptor blockDescriptor, ContentBlock block)
        {
            return builder =>
            {
                builder.OpenComponent(0, blockDescriptor.FormComponent);
                builder.AddAttribute(1, "FormOptions", FormOptions);
                builder.AddAttribute(2, "Block", block);
                builder.SetKey(block.Id);
                builder.CloseComponent();
            };
        }

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
