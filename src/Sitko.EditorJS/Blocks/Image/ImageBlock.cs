namespace Sitko.EditorJS.Blocks.Image;

[ContentBlock("image")]
public record ImageBlock<TData> : ContentBlock<ImageBlockData<TData>> where TData : class, new();
