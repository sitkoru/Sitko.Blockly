using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Paragraph;

public record ParagraphBlockData : ContentBlockData
{
    [JsonPropertyName("text")] public string Text { get; set; } = "";
}