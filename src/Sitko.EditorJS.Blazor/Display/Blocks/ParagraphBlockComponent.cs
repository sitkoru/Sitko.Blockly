using Sitko.EditorJS.Blocks.Paragraph;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class ParagraphBlockComponent<TListOptions> : BlockComponent<ParagraphBlock, TListOptions>
    where TListOptions : BlazorEditorJSListOptions;
