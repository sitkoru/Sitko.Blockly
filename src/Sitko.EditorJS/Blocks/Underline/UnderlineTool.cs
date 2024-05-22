namespace Sitko.EditorJS.Blocks.Underline;

[ContentBlock("Underline")]
public record UnderlineTool : ContentBlock;

public record UnderlineBlockOptions : ContentBlockOptions<UnderlineTool>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/underline@latest";
    public override string ClassName { get; set; } = "Underline";
}
