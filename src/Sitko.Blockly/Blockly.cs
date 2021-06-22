using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Sitko.Blockly
{
    public interface IBlockly<out TBlockDescriptor> where TBlockDescriptor : ContentBlockDescriptor
    {
        ContentBlock CreateBlock<TBlock>() where TBlock : ContentBlock;
        ContentBlock CreateBlock(Type blockType);

        TBlockDescriptor GetBlockDescriptor<TBlock>() where TBlock : ContentBlock;
        TBlockDescriptor GetBlockDescriptor(Type blockType);
        IEnumerable<TBlockDescriptor> Descriptors { get; }
    }

    public class Blockly<TBlockDescriptor> : IBlockly<TBlockDescriptor> where TBlockDescriptor : ContentBlockDescriptor
    {
        private readonly List<TBlockDescriptor> _blockDescriptors;
        private readonly ILogger<Blockly<TBlockDescriptor>> _logger;

        public Blockly(IEnumerable<TBlockDescriptor> blockDescriptors, ILogger<Blockly<TBlockDescriptor>> logger)
        {
            _blockDescriptors = blockDescriptors.ToList();
            _logger = logger;
        }

        public ContentBlock CreateBlock<TBlock>() where TBlock : ContentBlock
        {
            return CreateBlock(typeof(TBlock));
        }

        public ContentBlock CreateBlock(Type blockType)
        {
            var descriptor = GetBlockDescriptor(blockType);
            _logger.LogDebug("Create new block {Title}", descriptor.Title);
            var block = Activator.CreateInstance(descriptor.Type) as ContentBlock;
            block!.Id = Guid.NewGuid();
            return block;
        }

        public TBlockDescriptor GetBlockDescriptor<TBlock>() where TBlock : ContentBlock
        {
            return GetBlockDescriptor(typeof(TBlock));
        }

        public TBlockDescriptor GetBlockDescriptor(Type blockType)
        {
            if (!typeof(ContentBlock).IsAssignableFrom(blockType))
            {
                throw new ArgumentException($"Block type {blockType} doesn't inherits from ContentBlock");
            }

            var descriptor = _blockDescriptors.FirstOrDefault(d => d.Type == blockType);
            if (descriptor is null)
            {
                throw new Exception($"Can't find descriptor for {blockType}");
            }

            return descriptor;
        }

        public IEnumerable<TBlockDescriptor> Descriptors => _blockDescriptors;
    }
}
