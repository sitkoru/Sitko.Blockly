using Sitko.EditorJS.Blocks.Table;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class TableBlockComponent<TListOptions> : BlockComponent<TableBlock, TListOptions>
    where TListOptions : BlazorEditorJSListOptions;
