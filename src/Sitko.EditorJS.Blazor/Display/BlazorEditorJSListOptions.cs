using Sitko.Core.Storage;

namespace Sitko.EditorJS.Blazor.Display;

public class BlazorEditorJSListOptions : EditorJSListOptions
{
    public BlazorEditorJSListOptions(BlocksListMode mode = BlocksListMode.Full,
        IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
    {
    }
}
