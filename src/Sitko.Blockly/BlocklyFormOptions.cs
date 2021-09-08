using System;
using System.Collections.Generic;
using Sitko.Core.Storage;

namespace Sitko.Blockly
{
    public class BlocklyFormOptions
    {
        private readonly Dictionary<Type, int> maxBlockCounts = new();
        private readonly Dictionary<Type, int> blockPriorities = new();
        public int MaxBlocks { get; set; }
        private readonly HashSet<Type> allowedBlocks = new();
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
    }
}
