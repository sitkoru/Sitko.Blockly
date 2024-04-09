namespace Sitko.EditorJS.Blocks;

public interface IBlocksAccessor
{
    string GetConfig(Guid id);
    IReadOnlyDictionary<string, string> GetScripts();
    IReadOnlyDictionary<string, Type> GetBlockTypes();
}
