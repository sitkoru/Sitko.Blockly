using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Sitko.Blockly
{
    using System.Collections.Concurrent;
    using System.Reflection;

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
        protected static readonly ConcurrentDictionary<Type, IBlockDescriptor> StaticDescriptors = new();
        protected static readonly ConcurrentDictionary<Type, IContentBlockMetadata> StaticMetadata = new();

        public static IBlockDescriptor[] GetDescriptors() =>
            StaticDescriptors.Values.ToArray();

        public static IBlockDescriptor? GetDescriptor(string key) =>
            StaticDescriptors.Values.FirstOrDefault(d => d.Key == key);

        public static IBlockDescriptor? GetDescriptor(Type type) =>
            StaticDescriptors.Values.FirstOrDefault(d => d.Type == type);

        public static IContentBlockMetadata GetMetadata(IBlockDescriptor descriptor) =>
            StaticMetadata.Values.First(d => d.BlockType == descriptor.Type);
    }

    public class Blockly<TBlockDescriptor> : Blockly, IBlockly<TBlockDescriptor>
        where TBlockDescriptor : IBlockDescriptor
    {
        private readonly List<TBlockDescriptor> blockDescriptors;
        private readonly List<IContentBlockMetadata> blocksMetadata = new();
        private readonly ILogger<Blockly<TBlockDescriptor>> logger;

        public Blockly(IEnumerable<TBlockDescriptor> blockDescriptors, ILogger<Blockly<TBlockDescriptor>> logger)
        {
            this.blockDescriptors = blockDescriptors.ToList();
            this.logger = logger;
        }

        public ContentBlock CreateBlock<TBlock>() where TBlock : ContentBlock => CreateBlock(typeof(TBlock));

        public ContentBlock CreateBlock(Type blockType)
        {
            var descriptor = GetBlockDescriptor(blockType);
            return CreateBlock(descriptor);
        }

        public ContentBlock CreateBlock(TBlockDescriptor blockDescriptor)
        {
            logger.LogDebug("Create new block {Title}", blockDescriptor.Title);
            var block = Activator.CreateInstance(blockDescriptor.Type) as ContentBlock;
            block!.Id = Guid.NewGuid();
            return block;
        }

        public TBlockDescriptor GetBlockDescriptor<TBlock>() where TBlock : ContentBlock =>
            GetBlockDescriptor(typeof(TBlock));

        public TBlockDescriptor GetBlockDescriptor(Type blockType)
        {
            if (!typeof(ContentBlock).IsAssignableFrom(blockType))
            {
                throw new ArgumentException($"Block type {blockType} doesn't inherits from ContentBlock");
            }

            var descriptor = blockDescriptors.FirstOrDefault(d => d.Type == blockType);
            if (descriptor is null)
            {
                throw new InvalidOperationException($"Can't find descriptor for {blockType}");
            }

            return descriptor;
        }

        public IEnumerable<TBlockDescriptor> Descriptors => blockDescriptors;

        public Task InitAsync()
        {
            foreach (var blockDescriptor in blockDescriptors.Cast<IBlockDescriptor>())
            {
                StaticDescriptors.TryAdd(blockDescriptor.Type, blockDescriptor);
                var metadataAttribute = blockDescriptor.Type.GetCustomAttribute<ContentBlockMetadataAttribute>() ??
                                        new ContentBlockMetadataAttribute();
                blocksMetadata.Add(new ContentBlockMetadata(blockDescriptor.Type, metadataAttribute.Priority,
                    metadataAttribute.MaxCount));
            }

            foreach (var blockMetadata in blocksMetadata)
            {
                StaticMetadata.TryAdd(blockMetadata.BlockType, blockMetadata);
            }

            return Task.CompletedTask;
        }
    }
}
