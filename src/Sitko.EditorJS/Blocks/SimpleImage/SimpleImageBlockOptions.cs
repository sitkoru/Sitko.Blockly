namespace Sitko.EditorJS.Blocks.SimpleImage;

public record SimpleImageBlockOptions : ContentBlockOptions<SimpleImageBlock>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/simple-image@latest";
    public override string ClassName { get; set; } = "SimpleImage";
}
