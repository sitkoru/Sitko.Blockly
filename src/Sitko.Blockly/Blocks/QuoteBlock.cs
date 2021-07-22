using Sitko.Core.App.Localization;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks
{
    public record QuoteBlock : ContentBlock
    {
        public override string ToString() => $"{Author}: {Text} ({Link})";

        public string Text { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? Link { get; set; }
        public StorageItem? Picture { get; set; }
    }

    public record QuoteBlockDescriptor : BlockDescriptor<QuoteBlock>
    {
        public override int Priority => 4;

        public QuoteBlockDescriptor(ILocalizationProvider<QuoteBlock> localizationProvider) : base(localizationProvider)
        {
        }
    }
}
