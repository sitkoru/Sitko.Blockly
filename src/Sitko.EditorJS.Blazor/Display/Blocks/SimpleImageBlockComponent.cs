using Sitko.EditorJS.Blocks.SimpleImage;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class SimpleImageBlockComponent<TListOptions> : BlockComponent<SimpleImageBlock, TListOptions>
    where TListOptions : BlazorEditorJSListOptions;
