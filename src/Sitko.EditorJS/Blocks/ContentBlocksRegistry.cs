namespace Sitko.EditorJS.Blocks;

public static class ContentBlocksRegistry
{
    private static readonly Dictionary<string, ContentBlockRegistration> BlocksByKey = new();
    private static readonly Dictionary<Type, ContentBlockRegistration> BlocksByType = new();


    public static IReadOnlyDictionary<string, Type> GetBlockTypes() =>
        BlocksByType.ToDictionary(block => block.Value.Key, block => block.Key);

    public static void Register<TBlock, TBlockOptions>() where TBlock : ContentBlock
        where TBlockOptions : IContentBlockOptions<TBlock>
    {
        var attribute = typeof(TBlock).GetCustomAttributes(typeof(ContentBlockAttribute), false)
                            .OfType<ContentBlockAttribute>().FirstOrDefault() ??
                        throw new InvalidOperationException(
                            $"Class {typeof(TBlock)} should have {typeof(ContentBlockAttribute)} attribute");
        if (BlocksByType.ContainsKey(typeof(TBlock)))
        {
            throw new InvalidOperationException($"Block {typeof(TBlock)} already registered");
        }

        if (BlocksByKey.TryGetValue(attribute.Key, out var blockRegistration))
        {
            throw new InvalidOperationException(
                $"Block with key {attribute.Key} already registered: {blockRegistration.BlockType}");
        }

        var registration = new ContentBlockRegistration(attribute.Key, typeof(TBlock), typeof(TBlockOptions));
        BlocksByType[typeof(TBlock)] = registration;
        BlocksByKey[attribute.Key] = registration;
        //PolymorphismOptions.DerivedTypes.Add(new JsonDerivedType(typeof(TBlock), attribute.Key));
    }

    internal static ContentBlockRegistration GetKey<TBlock>() where TBlock : ContentBlock => GetKey(typeof(TBlock));
    internal static ContentBlockRegistration GetKey(Type blockType) => BlocksByType[blockType];

    internal static ContentBlockRegistration GetBlockMetadata(string key) => BlocksByKey[key];

    internal static void Clear()
    {
        BlocksByType.Clear();
        BlocksByKey.Clear();
    }
}

// TODO: In .NET 9 we can use this with https://github.com/dotnet/runtime/issues/72604
