using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Paragraph;

public record ParagraphBlockConfig : ContentBlockConfig
{
    [JsonPropertyName("placeholder")] public string Placeholder { get; set; } = "";

    [JsonPropertyName("preserveBlank")] public bool PreserveBlank { get; set; } = false;
}