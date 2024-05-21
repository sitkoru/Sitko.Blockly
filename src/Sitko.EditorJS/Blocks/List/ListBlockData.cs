using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.List;

public record ListBlockData : ContentBlockData
{
    [JsonPropertyName("style")] public string Style { get; set; } = "";
    [JsonPropertyName("items")] public string[] Items { get; set; } = Array.Empty<string>();
}
