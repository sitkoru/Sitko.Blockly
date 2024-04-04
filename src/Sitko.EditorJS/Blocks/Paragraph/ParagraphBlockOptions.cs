namespace Sitko.EditorJS.Blocks.Paragraph;

public record ParagraphBlockOptions : ContentBlockOptions<ParagraphBlock, ParagraphBlockConfig>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/paragraph@latest";
    public override string ClassName { get; set; } = "Paragraph";
}