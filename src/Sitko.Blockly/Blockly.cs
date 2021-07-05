using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sitko.Blockly
{
    public interface IBlockly<TBlockDescriptor> where TBlockDescriptor : IBlockDescriptor
    {
        ContentBlock CreateBlock<TBlock>() where TBlock : ContentBlock;
        ContentBlock CreateBlock(Type blockType);
        ContentBlock CreateBlock(TBlockDescriptor blockDescriptor);

        TBlockDescriptor GetBlockDescriptor<TBlock>() where TBlock : ContentBlock;
        TBlockDescriptor GetBlockDescriptor(Type blockType);
        IEnumerable<TBlockDescriptor> Descriptors { get; }
        Task InitAsync();
    }

    public class Blockly
    {
        protected static readonly List<IBlockDescriptor> StaticDescriptors = new();

        public static IBlockDescriptor? GetDescriptor(string key)
        {
            return StaticDescriptors.FirstOrDefault(d => d.Key == key);
        }
        
        public static IBlockDescriptor? GetDescriptor(Type type)
        {
            return StaticDescriptors.FirstOrDefault(d => d.Type == type);
        }
    }

    public class Blockly<TBlockDescriptor> : Blockly, IBlockly<TBlockDescriptor>
        where TBlockDescriptor : IBlockDescriptor
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
            return CreateBlock(descriptor);
        }

        public ContentBlock CreateBlock(TBlockDescriptor blockDescriptor)
        {
            _logger.LogDebug("Create new block {Title}", blockDescriptor.Title);
            var block = Activator.CreateInstance(blockDescriptor.Type) as ContentBlock;
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
        public Task InitAsync()
        {
            StaticDescriptors.AddRange(_blockDescriptors.Cast<IBlockDescriptor>());
            return Task.CompletedTask;
        }
    }
}
