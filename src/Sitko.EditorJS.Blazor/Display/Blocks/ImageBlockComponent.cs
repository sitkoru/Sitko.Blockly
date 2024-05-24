using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class ImageBlockComponent<TData, TListOptions> : BlockComponent<ImageBlock<TData>, TListOptions>
    where TData : class, new()
    where TListOptions : BlazorEditorJSListOptions;
