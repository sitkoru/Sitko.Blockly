using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly.Blazor.Forms
{
    public abstract class BlocklyForm<TEntity, TForm, TOptions> : InputBase<List<ContentBlock>>
        where TForm : BaseForm<TEntity>
        where TEntity : class
        where TOptions : BlazorBlocklyFormOptions, new()
    {
        [Parameter] public TForm Form { get; set; } = null!;
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;

        public IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;

        protected readonly OrderedCollection<ContentBlock> Blocks = new();

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
            Blocks.SetItems(CurrentValue?.OrderBy(b => b.Position) ?? new List<ContentBlock>().AsEnumerable());
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
                var block = Blockly.CreateBlock(blockDescriptor.Type);
                Blocks.AddItem(block, neighbor, after);
                UpdateForm();
            }
        }

        protected void MoveBlockUp(ContentBlock block)
        {
            Blocks.MoveUp(block);
            UpdateForm();
        }


        protected void MoveBlockDown(ContentBlock block)
        {
            Blocks.MoveDown(block);
            UpdateForm();
        }

        protected void DeleteBlock(ContentBlock block)
        {
            Blocks.RemoveItem(block);
            UpdateForm();
        }

        private void UpdateForm()
        {
            CurrentValue = new List<ContentBlock>(Blocks.ToList());
            Form.NotifyChange();
        }

        public RenderFragment RenderBlockForm(IBlazorBlockDescriptor blockDescriptor, ContentBlock block)
        {
            return builder =>
            {
                builder.OpenComponent(0, blockDescriptor.FormComponent);
                builder.AddAttribute(1, "FormOptions", FormOptions);
                builder.AddAttribute(2, "Block", block);
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
    }
}
