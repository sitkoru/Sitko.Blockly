namespace Sitko.EditorJS.Blazor.Display;

public class EditorJSListOptions
{
    public EditorJSListOptions(BlocksListMode mode = BlocksListMode.Full, string? entityUrl = null)
    {
        Mode = mode;
        EntityUrl = entityUrl;
    }
    public BlocksListMode Mode { get; }
    public string? EntityUrl { get; }
}

public enum BlocksListMode
{
    Preview,
    Full
}
