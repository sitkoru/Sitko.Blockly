namespace Sitko.EditorJS.Blocks.Quote;

public record QuoteBlockConfig : ContentBlockConfig
{
    public string QuotePlaceholder { get; set; } = "";
    public string CaptionPlaceholder { get; set; } = "";
}
