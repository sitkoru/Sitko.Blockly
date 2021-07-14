using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks
{
    public record IframeBlock : ContentBlock
    {
        public override string ToString() => $"Frame: {Src}";

        public string Src { get; set; } = "";
    }

    public record IframeBlockDescriptor : BlockDescriptor<IframeBlock>
    {
        public override int Priority => 9;

        public IframeBlockDescriptor(ILocalizationProvider<IframeBlock> localizationProvider) : base(
            localizationProvider)
        {
        }
    }
}
