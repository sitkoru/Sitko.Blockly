using System;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesign.Forms
{
    public abstract class BlocklyFormBase : ComponentBase
    {
        [Parameter] public IBlocklyEntity Entity { get; set; }

        [Inject] protected IBlockly<AntDesignContentBlockDescriptor> Blockly { get; set; }

        protected ObservableCollection<ContentBlock> Blocks;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            if (!Entity.Blocks.Any())
            {
                Entity.Blocks.Add(new TextBlock());
            }

            Blocks = new ObservableCollection<ContentBlock>(Entity.Blocks.OrderBy(b => b.Position));
        }

        protected void AddBlock(Type blockType, ContentBlock? neighbor = null, bool after = true)
        {
            var block = Blockly.CreateBlock(blockType);
            var position = 0;
            if (neighbor != null)
            {
                position = after ? neighbor.Position + 1 : neighbor.Position;
            }

            Blocks.Insert(position, block);
            FillPositions();
        }

        protected bool CanMoveBlockUp(ContentBlock block)
        {
            return block.Position > 0;
        }

        protected bool CanMoveBlockDown(ContentBlock block)
        {
            return block.Position < Blocks.Count - 1;
        }

        protected void MoveBlockUp(ContentBlock block)
        {
            if (CanMoveBlockUp(block))
            {
                UpdateIndex(block.Position - 1, block.Position);
            }
        }


        protected void MoveBlockDown(ContentBlock block)
        {
            if (CanMoveBlockDown(block))
            {
                UpdateIndex(block.Position + 1, block.Position);
            }
        }

        protected void UpdateIndex(int newIndex, int oldIndex)
        {
            Blocks.Move(oldIndex, newIndex);
            FillPositions();
        }

        protected void FillPositions()
        {
            foreach (var block in Blocks)
            {
                block.Position = Blocks.IndexOf(block);
            }

            Entity.Blocks = Blocks.ToList();
        }

        protected void DeleteBlock(ContentBlock block)
        {
            Blocks.Remove(block);
            if (!Blocks.Any())
            {
                AddBlock(typeof(TextBlock));
            }

            FillPositions();
        }
    }
}
