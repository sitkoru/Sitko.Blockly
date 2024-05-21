namespace Sitko.EditorJS.Blocks.Table;

public record TableBlockOptions : ContentBlockOptions<TableBlock, TableBlockConfig>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/table@latest";
    public override string ClassName { get; set; } = "Table";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          rows: "{{Config.Rows}}",
                                                          cols: "{{Config.Cols}}",
                                                          withHeadings: "{{Config.WithHeadings}}"
                                                          """;
}
