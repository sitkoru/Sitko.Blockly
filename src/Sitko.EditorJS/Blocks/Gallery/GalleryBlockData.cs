using System.Text.Json.Serialization;
using Sitko.EditorJS.Data;

namespace Sitko.EditorJS.Blocks.Gallery;

public record GalleryBlockData<TData> : BaseImageBlockData where TData : class, new()
{
    [JsonPropertyName("files")] public ImageBlockDataFile<TData>[] Files { get; set; } = Array.Empty<ImageBlockDataFile<TData>>();
    [JsonPropertyName("source")] public string Source { get; set; } = "";
    [JsonPropertyName("style")] public string Style { get; set; } = "";
}
