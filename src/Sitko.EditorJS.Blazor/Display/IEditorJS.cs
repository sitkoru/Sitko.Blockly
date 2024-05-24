using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public interface IEditorJS<TBlockDescriptor> where TBlockDescriptor : IBlockDescriptor
{
    IEnumerable<TBlockDescriptor> Descriptors { get; }

    TBlockDescriptor GetBlockDescriptor<TBlock>() where TBlock : ContentBlock;
    TBlockDescriptor GetBlockDescriptor(Type blockType);
    Task InitAsync();
}
public class EditorJS
{
    protected static readonly ConcurrentDictionary<Type, IBlockDescriptor> StaticDescriptors = new();

    public static IBlockDescriptor[] GetDescriptors() =>
        StaticDescriptors.Values.ToArray();

    public static IBlockDescriptor? GetDescriptor(string key) =>
        StaticDescriptors.Values.FirstOrDefault(d => d.Key == key);

    public static IBlockDescriptor? GetDescriptor(Type type) =>
        StaticDescriptors.Values.FirstOrDefault(d => d.Type == type);
}

public class EditorJS<TBlockDescriptor> : EditorJS, IEditorJS<TBlockDescriptor>
    where TBlockDescriptor : IBlockDescriptor
{
    private readonly List<TBlockDescriptor> blockDescriptors;
    private readonly ILogger<EditorJS<TBlockDescriptor>> logger;

    public EditorJS(IEnumerable<TBlockDescriptor> blockDescriptors, ILogger<EditorJS<TBlockDescriptor>> logger)
    {
        this.blockDescriptors = blockDescriptors.ToList();
        this.logger = logger;
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
        }

        return Task.CompletedTask;
    }
}
