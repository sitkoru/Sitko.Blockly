using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Table;

public record TableBlockData : ContentBlockData
{
    [JsonPropertyName("withHeadings")] public bool WithHeadings { get; set; }
    [JsonPropertyName("content")] public string[][]? Content { get; set; }
}
