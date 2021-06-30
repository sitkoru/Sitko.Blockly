using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly.Blazor.Forms
{
    public abstract class BlocklyForm<TEntity, TForm> : InputBase<List<ContentBlock>> where TForm : BaseForm<TEntity>
        where TEntity : class
    {
        [Parameter] public TForm Form { get; set; } = null!;
        [Parameter] public List<Type>? AllowedBlocks { get; set; }
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;

        public IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;

        protected readonly OrderedCollection<ContentBlock> Blocks = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!CurrentValue.Any())
            {
                CurrentValue.Add(new TextBlock());
            }

            BlockDescriptors = Blockly.Descriptors
                .Where(d => AllowedBlocks is null || AllowedBlocks.Contains(d.Type)).ToArray();
            Blocks.SetItems(CurrentValue.OrderBy(b => b.Position));
        }

        protected void AddBlock(Type blockType, ContentBlock? neighbor = null, bool after = true)
        {
            var block = Blockly.CreateBlock(blockType);
            Blocks.AddItem(block, neighbor, after);
            UpdateForm();
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
                var formType = blockDescriptor.FormComponent;
                if (blockDescriptor.FormComponent.IsGenericTypeDefinition)
                {
                    formType = blockDescriptor.FormComponent.MakeGenericType(typeof(TForm));
                }

                builder.OpenComponent(0, formType);
                builder.AddAttribute(1, "Form", Form);
                builder.AddAttribute(2, "Block", block);
                builder.CloseComponent();
            };
        }

        protected override bool TryParseValueFromString(string value, out List<ContentBlock> result,
            out string validationErrorMessage)
        {
            result = default!;
            validationErrorMessage = "";
            return false;
        }
    }
}
