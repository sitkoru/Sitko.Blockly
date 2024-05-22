using System.Text.Json.Serialization;
using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blocks.Gallery;

public record GalleryBlockData<TData> : ContentBlockData where TData : class, new()
{
    [JsonPropertyName("files")] public ImageBlockDataFile<TData>[] Files { get; set; } = Array.Empty<ImageBlockDataFile<TData>>();
    [JsonPropertyName("source")] public string Source { get; set; } = "";
    [JsonPropertyName("style")] public string Style { get; set; } = "";
}
