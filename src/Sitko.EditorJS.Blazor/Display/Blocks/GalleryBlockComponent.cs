using Sitko.EditorJS.Blocks.Gallery;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class GalleryBlockComponent<TData> : BlockComponent<GalleryBlock<TData>>
    where TData : class, new();
