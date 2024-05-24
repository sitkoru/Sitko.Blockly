namespace Sitko.EditorJS.Blocks.Quote;

public record QuoteBlockOptions : ContentBlockOptions<QuoteBlock, QuoteBlockConfig>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/quote@latest";
    public override string ClassName { get; set; } = "Quote";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          quotePlaceholder: "{{Config.QuotePlaceholder}}",
                                                          captionPlaceholder: "{{Config.CaptionPlaceholder}}"
                                                          """;
}
