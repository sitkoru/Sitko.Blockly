using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Quote;

public record QuoteBlockData : ContentBlockData
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
    [JsonPropertyName("caption")] public string Caption { get; set; } = "";
    [JsonPropertyName("alignment")] public string Alignment { get; set; } = "";
}
