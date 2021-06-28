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
    public abstract class BlocklyForm<TEntity, TForm> : ComponentBase where TForm : BaseForm<TEntity>, IBlocklyForm
        where TEntity : class, IBlocklyEntity
    {
        [Parameter] public TForm Form { get; set; } = null!;
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;

        public BlazorContentBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<BlazorContentBlockDescriptor>();

        [Inject] protected IBlockly<BlazorContentBlockDescriptor> Blockly { get; set; } = null!;

        protected readonly OrderedCollection<ContentBlock> Blocks = new();

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!Form.Blocks.Any())
            {
                Form.Blocks.Add(new TextBlock());
            }

            BlockDescriptors = Blockly.Descriptors
                .Where(d => Form.AllowedBlocks is null || Form.AllowedBlocks.Contains(d.Type)).ToArray();
            Blocks.SetItems(Form.Blocks.OrderBy(b => b.Position));
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
            Form.Blocks = new List<ContentBlock>(Blocks.ToList());
            Form.NotifyChange();
        }
    }
}
