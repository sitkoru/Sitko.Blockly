using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Image;

public record ImageBlockData<TData> : ContentBlockData where TData : class, new()
{
    [JsonPropertyName("caption")] public string Caption { get; set; } = "";
    [JsonPropertyName("stretched")] public bool Stretched { get; set; }
    [JsonPropertyName("withBackground")] public bool WithBackground { get; set; }
    [JsonPropertyName("withBorder")] public bool WithBorder { get; set; }
    [JsonPropertyName("file")] public ImageBlockDataFile<TData> File { get; set; }
}

public record ImageBlockDataFile<TData> where TData : class, new()
{
    [JsonPropertyName("url")] public string Url { get; set; } = "";
    [JsonPropertyName("data")] public TData Data { get; set; } = new();
}
