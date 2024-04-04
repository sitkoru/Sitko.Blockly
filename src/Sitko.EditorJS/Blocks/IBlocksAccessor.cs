namespace Sitko.EditorJS.Blocks;

public interface IBlocksAccessor
{
    EditorJSConfig GetConfig(string holder);
    IReadOnlyDictionary<string, string> GetScripts();
    IReadOnlyDictionary<string, Type> GetBlockTypes();
}