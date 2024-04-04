using System.Text.Json.Serialization;
using Sitko.EditorJS.Blocks;
using Sitko.EditorJS.Helpers;

namespace Sitko.EditorJS.Data;

public record EditorJSData
{
    [JsonPropertyName("time")] public long Time { get; set; }
    [JsonPropertyName("version")] public string Version { get; set; } = "unknown";
    [JsonPropertyName("blocks")] public ValueCollection<ContentBlock> Blocks { get; set; } = new();
}