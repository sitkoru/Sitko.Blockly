namespace Sitko.EditorJS.Blocks;

internal class BlocksAccessor(IEnumerable<IContentBlockAccessor> blockAccessors) : IBlocksAccessor
{
    private readonly IContentBlockAccessor[] blockAccessors = blockAccessors.ToArray();

    public EditorJSConfig GetConfig(string holder)
    {
        var config = new EditorJSConfig { Holder = holder };
        foreach (var blockOptionsAccessor in blockAccessors)
        {
            config.Tools[blockOptionsAccessor.Key] = blockOptionsAccessor.Options.GetConfig();
        }

        return config;
    }

    public IReadOnlyDictionary<string, string> GetScripts() =>
        blockAccessors.ToDictionary(accessor => accessor.Key, accessor => accessor.Options.ScriptUrl);

    public IReadOnlyDictionary<string, Type> GetBlockTypes() =>
        blockAccessors.ToDictionary(block => block.Key, block => block.Type);
}