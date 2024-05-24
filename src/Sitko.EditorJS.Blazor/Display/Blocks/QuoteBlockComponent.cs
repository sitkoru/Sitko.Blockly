using Sitko.EditorJS.Blocks.Quote;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class QuoteBlockComponent<TListOptions> : BlockComponent<QuoteBlock, TListOptions>
    where TListOptions : BlazorEditorJSListOptions;
