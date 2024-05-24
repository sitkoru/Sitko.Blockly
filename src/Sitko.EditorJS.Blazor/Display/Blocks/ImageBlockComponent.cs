using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blazor.Display.Blocks;

public abstract class ImageBlockComponent<TData> : BlockComponent<ImageBlock<TData>>
    where TData : class, new();
