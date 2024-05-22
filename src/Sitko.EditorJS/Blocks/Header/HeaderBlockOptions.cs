using System.Globalization;

namespace Sitko.EditorJS.Blocks.Header;

public record HeaderBlockOptions : ContentBlockOptions<HeaderBlock, HeaderBlockConfig>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/header@latest";
    public override string ClassName { get; set; } = "Header";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          placeholder: "{{Config.Placeholder}}",
                                                          levels: {{Config.Levels}},
                                                          defaultLevel: {{Config.DefaultLevel.ToString(CultureInfo.InvariantCulture)}}
                                                          """;
}
