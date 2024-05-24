using Sitko.EditorJS.Blocks.Header;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class HeaderBlockComponent<TListOptions> : BlockComponent<HeaderBlock, TListOptions>
    where TListOptions : BlazorEditorJSListOptions;
