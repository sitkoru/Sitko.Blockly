using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.SimpleImage;

public record SimpleImageBlockData : ContentBlockData
{
    [JsonPropertyName("url")] public string Url { get; set; } = "";

    [JsonPropertyName("caption")] public string Caption { get; set; } = "";

    [JsonPropertyName("withBorder")] public bool? WithBorder { get; set; }

    [JsonPropertyName("withBackground")] public bool? WithBackground { get; set; }
    [JsonPropertyName("stretched")] public bool? Stretched { get; set; }
}
