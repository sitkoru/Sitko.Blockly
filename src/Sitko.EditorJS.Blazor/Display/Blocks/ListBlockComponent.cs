using Sitko.EditorJS.Blocks.List;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class ListBlockComponent<TListOptions> : BlockComponent<ListBlock, TListOptions>
    where TListOptions : BlazorEditorJSListOptions;
