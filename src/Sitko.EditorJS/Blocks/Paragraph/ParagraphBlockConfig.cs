namespace Sitko.EditorJS.Blocks.Paragraph;

public record ParagraphBlockConfig : ContentBlockConfig
{
    public string Placeholder { get; set; } = "";
    public bool PreserveBlank { get; set; }
}
