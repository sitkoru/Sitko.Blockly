using Sitko.EditorJS.Blocks.Gallery;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class GalleryBlockComponent<TData, TListOptions> : BlockComponent<GalleryBlock<TData>, TListOptions>
    where TData : class, new()
    where TListOptions : BlazorEditorJSListOptions;
