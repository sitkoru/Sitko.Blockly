namespace Sitko.EditorJS.Blocks;

internal class BlocksAccessor(IEnumerable<IContentBlockAccessor> blockAccessors) : IBlocksAccessor
{
    private readonly IContentBlockAccessor[] blockAccessors = blockAccessors.ToArray();

    public string GetConfig(Guid id)
    {
        var configs = new List<string>();
        foreach (var blockOptionsAccessor in blockAccessors)
        {
            configs.Add($"\"{blockOptionsAccessor.Key}\": {blockOptionsAccessor.Options.GetConfig(id)}");
        }

        return string.Join(",", configs);
    }

    public IReadOnlyDictionary<string, string> GetScripts() =>
        blockAccessors.ToDictionary(accessor => accessor.Key, accessor => accessor.Options.ScriptUrl);

    public IReadOnlyDictionary<string, Type> GetBlockTypes() =>
        blockAccessors.ToDictionary(block => block.Key, block => block.Type);
}
