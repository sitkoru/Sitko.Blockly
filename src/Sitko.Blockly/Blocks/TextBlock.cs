using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks
{
    public record TextBlock : ContentBlock
    {
        public override string ToString() => Text;

        public string Text { get; set; } = "";
    }

    public record TextBlockDescriptor : BlockDescriptor<TextBlock>
    {
        public override int Priority => 1;

        public TextBlockDescriptor(ILocalizationProvider<TextBlock> localizationProvider) : base(localizationProvider)
        {
        }
    }
}
