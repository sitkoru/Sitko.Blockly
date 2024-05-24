using Sitko.Core.Storage;
using Sitko.EditorJS.Blazor.Display;

namespace Sitko.EditorJS.MudBlazor.Display;

public class MudBlazorEditorJSListOptions : BlazorEditorJSListOptions
{
    public MudBlazorEditorJSListOptions(BlocksListMode mode = BlocksListMode.Full,
        IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
    {
    }
}
