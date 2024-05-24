using Sitko.EditorJS.Blazor.Display;

namespace Sitko.EditorJS.MudBlazor.Display;

public class MudBlazorEditorJSListOptions : BlazorEditorJSListOptions
{
    public MudBlazorEditorJSListOptions(BlocksListMode mode = BlocksListMode.Full, string? entityUrl = null) : base(mode, entityUrl)
    {
    }
}
