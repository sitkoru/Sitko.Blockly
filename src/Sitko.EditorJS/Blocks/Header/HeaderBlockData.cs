using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Header;

public record HeaderBlockData : ContentBlockData
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
    [JsonPropertyName("level")] public int Level { get; set; }
}
