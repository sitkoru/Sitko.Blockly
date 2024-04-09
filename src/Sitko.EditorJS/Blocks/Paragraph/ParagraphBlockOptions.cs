using Sitko.EditorJS.Helpers;

namespace Sitko.EditorJS.Blocks.Paragraph;

public record ParagraphBlockOptions : ContentBlockOptions<ParagraphBlock, ParagraphBlockConfig>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/paragraph@latest";
    public override string ClassName { get; set; } = "Paragraph";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          placeholder: "{{Config.Placeholder}}",
                                                          preserveBlank: {{Config.PreserveBlank.AsString()}}
                                                          """;
}
