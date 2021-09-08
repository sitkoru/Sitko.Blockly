using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks
{
    [ContentBlockMetadata(9)]
    public record IframeBlock : ContentBlock
    {
        public override string ToString() => $"Frame: {Src}";

        public string Src { get; set; } = "";
    }

    public record IframeBlockDescriptor : BlockDescriptor<IframeBlock>
    {
        public IframeBlockDescriptor(ILocalizationProvider<IframeBlock> localizationProvider) : base(
            localizationProvider)
        {
        }
    }
}
