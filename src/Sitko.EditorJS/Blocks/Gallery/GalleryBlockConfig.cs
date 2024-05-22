using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blocks.Gallery;

public record GalleryBlockConfig : ImageBlockConfig
{
    public int MaxElementCount { get; set; } = 10;
}
