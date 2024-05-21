namespace Sitko.EditorJS.Blocks.List;

public record ListBlockOptions : ContentBlockOptions<ListBlock, ListBlockConfig>
{

    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/list@latest";
    public override string ClassName { get; set; } = "List";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          defaultStyle: "{{Config.DefaultStyle}}"
                                                          """;
}
