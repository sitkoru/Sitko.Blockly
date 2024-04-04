namespace Sitko.EditorJS.Blocks;

internal interface IContentBlockAccessor
{
    public Type Type { get; }
    public IContentBlockOptions Options { get; }
    public string Key { get; }
}