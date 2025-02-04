namespace Sitko.EditorJS.Blocks.Strikethrough;

[ContentBlock("Strikethrough")]
public record StrikethroughBlock : ContentBlock;

public record StrikethroughBlockOptions : ContentBlockOptions<StrikethroughBlock>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@sotaproject/strikethrough@latest";
    public override string ClassName { get; set; } = "Strikethrough";
}
