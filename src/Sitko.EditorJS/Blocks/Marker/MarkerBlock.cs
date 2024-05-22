namespace Sitko.EditorJS.Blocks.Marker;

[ContentBlock("Marker")]
public record MarkerBlock : ContentBlock;

public record MarkerBlockOptions : ContentBlockOptions<MarkerBlock>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/marker@latest";
    public override string ClassName { get; set; } = "Marker";
}
