namespace Sitko.EditorJS.Blocks.Gallery;

[ContentBlock("ImageGallery")]
public record GalleryBlock<TData> : ContentBlock<GalleryBlockData<TData>> where TData : class, new();
