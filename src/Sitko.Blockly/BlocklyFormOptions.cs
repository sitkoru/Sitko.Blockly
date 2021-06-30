using System;
using System.Collections.Generic;
using Sitko.Core.Storage;

namespace Sitko.Blockly
{
    public class BlocklyFormOptions
    {
        private readonly Dictionary<Type, int> _maxBlockCounts = new();
        private readonly Dictionary<Type, int> _blockPriorities = new();
        public int MaxBlocks { get; set; }
        private readonly HashSet<Type> _allowedBlocks = new();
        public IEnumerable<Type> AllowedBlocks => _allowedBlocks;

        public IStorage? Storage { get; set; }

        public int? MaxBlockCount(IBlockDescriptor descriptor)
        {
            if (_maxBlockCounts.ContainsKey(descriptor.Type))
            {
                return _maxBlockCounts[descriptor.Type];
            }

            return null;
        }

        public int? BlockPriority(IBlockDescriptor descriptor)
        {
            if (_blockPriorities.ContainsKey(descriptor.Type))
            {
                return _blockPriorities[descriptor.Type];
            }

            return null;
        }

        public BlocklyFormOptions ClearBlockMaxCounts()
        {
            _maxBlockCounts.Clear();
            return this;
        }

        public BlocklyFormOptions ConfigureBlockMaxCount<TBlock>(int maxCount) where TBlock : ContentBlock
        {
            _maxBlockCounts.TryAdd(typeof(TBlock), maxCount);
            return this;
        }

        public BlocklyFormOptions ClearBlockPriorities()
        {
            _blockPriorities.Clear();
            return this;
        }

        public BlocklyFormOptions ConfigureBlockPriority<TBlock>(int priority) where TBlock : ContentBlock
        {
            _blockPriorities.TryAdd(typeof(TBlock), priority);
            return this;
        }

        public BlocklyFormOptions ClearAllowedBlocks()
        {
            _allowedBlocks.Clear();
            return this;
        }

        public BlocklyFormOptions SetAllowedBlocks(IEnumerable<Type> allowedBlocks)
        {
            ClearAllowedBlocks();
            foreach (var allowedBlock in allowedBlocks)
            {
                AddAllowedBlock(allowedBlock);
            }

            return this;
        }

        public BlocklyFormOptions AddAllowedBlock(Type allowedBlock)
        {
            _allowedBlocks.Add(allowedBlock);
            return this;
        }

        public BlocklyFormOptions AddAllowedBlock<TBlock>() where TBlock : ContentBlock
        {
            _allowedBlocks.Add(typeof(TBlock));
            return this;
        }
    }
}
