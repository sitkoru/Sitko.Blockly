using System;
using System.Collections.Generic;
using Sitko.Core.Storage;
using JetBrains.Annotations;

namespace Sitko.Blockly
{
    [PublicAPI]
    public class BlocklyFormOptions
    {
        private readonly Dictionary<Type, int> maxBlockCounts = new();
        private readonly Dictionary<Type, int> blockPriorities = new();
        public int MaxBlocks { get; set; }
        private readonly HashSet<Type> allowedBlocks = new();
        private readonly HashSet<Type> disabledMoveBlocks = new();
        private readonly HashSet<Type> disabledDeleteBlocks = new();
        public IEnumerable<Type> AllowedBlocks => allowedBlocks;
        public IStorage? Storage { get; set; }

        public int MaxBlockCount(IBlockDescriptor descriptor)
        {
            if (maxBlockCounts.ContainsKey(descriptor.Type))
            {
                return maxBlockCounts[descriptor.Type];
            }

            return Blockly.GetMetadata(descriptor).MaxCount;
        }

        public int BlockPriority(IBlockDescriptor descriptor)
        {
            if (blockPriorities.ContainsKey(descriptor.Type))
            {
                return blockPriorities[descriptor.Type];
            }

            return Blockly.GetMetadata(descriptor).Priority;
        }

        public BlocklyFormOptions ClearBlockMaxCounts()
        {
            maxBlockCounts.Clear();
            return this;
        }

        public BlocklyFormOptions ConfigureBlockMaxCount<TBlock>(int maxCount) where TBlock : ContentBlock
        {
            maxBlockCounts.TryAdd(typeof(TBlock), maxCount);
            return this;
        }

        public BlocklyFormOptions ClearBlockPriorities()
        {
            blockPriorities.Clear();
            return this;
        }

        public BlocklyFormOptions ConfigureBlockPriority<TBlock>(int priority) where TBlock : ContentBlock
        {
            blockPriorities.TryAdd(typeof(TBlock), priority);
            return this;
        }

        public BlocklyFormOptions ClearAllowedBlocks()
        {
            allowedBlocks.Clear();
            return this;
        }

        public BlocklyFormOptions SetAllowedBlocks(IEnumerable<Type> blocks)
        {
            ClearAllowedBlocks();
            foreach (var allowedBlock in blocks)
            {
                AddAllowedBlock(allowedBlock);
            }

            return this;
        }

        public BlocklyFormOptions AddAllowedBlock(Type allowedBlock)
        {
            allowedBlocks.Add(allowedBlock);
            return this;
        }

        public BlocklyFormOptions AddAllowedBlock<TBlock>() where TBlock : ContentBlock
        {
            allowedBlocks.Add(typeof(TBlock));
            return this;
        }

        public BlocklyFormOptions DisableBlocksMove(IEnumerable<Type> blocks)
        {
            foreach (var disabledMoveBlock in blocks)
            {
                DisableBlockMove(disabledMoveBlock);
            }

            return this;
        }

        public BlocklyFormOptions DisableBlockMove(Type allowedBlock)
        {
            disabledMoveBlocks.Add(allowedBlock);
            return this;
        }

        public BlocklyFormOptions DisableBlockMove<TBlock>() where TBlock : ContentBlock
        {
            disabledMoveBlocks.Add(typeof(TBlock));
            return this;
        }

        public BlocklyFormOptions DisableBlocksDelete(IEnumerable<Type> blocks)
        {
            foreach (var disabledDeleteBlock in blocks)
            {
                DisableBlockDelete(disabledDeleteBlock);
            }

            return this;
        }

        public BlocklyFormOptions DisableBlockDelete(Type allowedBlock)
        {
            disabledDeleteBlocks.Add(allowedBlock);
            return this;
        }

        public BlocklyFormOptions DisableBlockDelete<TBlock>() where TBlock : ContentBlock
        {
            disabledDeleteBlocks.Add(typeof(TBlock));
            return this;
        }

        public bool CanDeleteBlock(ContentBlock block) => !disabledDeleteBlocks.Contains(block.GetType());

        public bool CanMoveBlock(ContentBlock block) => !disabledMoveBlocks.Contains(block.GetType());
    }
}
