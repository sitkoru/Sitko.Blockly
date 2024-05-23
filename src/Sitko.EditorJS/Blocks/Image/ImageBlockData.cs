using System.Text.Json.Serialization;
using Sitko.EditorJS.Data;

namespace Sitko.EditorJS.Blocks.Image;

public record ImageBlockData<TData> : BaseImageBlockData where TData : class, new()
{
    [JsonPropertyName("file")] public ImageBlockDataFile<TData> File { get; set; }
}
