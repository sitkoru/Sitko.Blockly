namespace Sitko.EditorJS.Blocks.Underline;

[ContentBlock("Underline")]
public record UnderlineBlock : ContentBlock;

public record UnderlineBlockOptions : ContentBlockOptions<UnderlineBlock>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/underline@latest";
    public override string ClassName { get; set; } = "Underline";
}
