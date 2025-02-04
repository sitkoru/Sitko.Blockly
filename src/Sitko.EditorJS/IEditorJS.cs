using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS;

public interface IEditorJS<TBlockDescriptor> where TBlockDescriptor : IBlockDescriptor
{
    IEnumerable<TBlockDescriptor> Descriptors { get; }

    TBlockDescriptor GetBlockDescriptor(Type blockType);
}

public class EditorJS<TBlockDescriptor>(IEnumerable<TBlockDescriptor> blockDescriptors) : IEditorJS<TBlockDescriptor>
    where TBlockDescriptor : IBlockDescriptor
{
    private readonly List<TBlockDescriptor> blockDescriptors = blockDescriptors.ToList();

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
}
