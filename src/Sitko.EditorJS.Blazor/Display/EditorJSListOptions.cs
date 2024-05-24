using Sitko.Core.Storage;

namespace Sitko.EditorJS.Blazor.Display;

public class EditorJSListOptions
{
    public EditorJSListOptions(BlocksListMode mode = BlocksListMode.Full, IStorage? storage = null,
        string? entityUrl = null)
    {
        Storage = storage;
        Mode = mode;
        EntityUrl = entityUrl;
    }

    public IStorage? Storage { get; }
    public BlocksListMode Mode { get; }
    public string? EntityUrl { get; }
}

public enum BlocksListMode
{
    Preview,
    Full
}
