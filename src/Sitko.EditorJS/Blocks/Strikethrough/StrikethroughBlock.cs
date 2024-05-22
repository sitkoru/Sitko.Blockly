namespace Sitko.EditorJS.Blocks.Strikethrough;

[ContentBlock("Strikethrough")]
public record StrikethroughBlock : ContentBlock;

public record StrikethroughBlockOptions : ContentBlockOptions<StrikethroughBlock>
{
    public override string ScriptUrl { get; set; } = "/_content/Sitko.EditorJS.Blazor/strikethrough.js";
    public override string ClassName { get; set; } = "Strikethrough";
}
