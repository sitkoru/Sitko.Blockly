using System.Text.Json.Serialization;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Data;

public record BaseImageBlockData : ContentBlockData
{
    [JsonPropertyName("caption")] public string Caption { get; set; } = "";
    [JsonPropertyName("stretched")] public bool Stretched { get; set; }
    [JsonPropertyName("withBackground")] public bool WithBackground { get; set; }
    [JsonPropertyName("withBorder")] public bool WithBorder { get; set; }
}
