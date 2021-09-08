using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks
{
    using Display;

    [ContentBlockMetadata(2, 1)]
    public record CutBlock : ContentBlock

    {
        public override string ToString() => "";

        public string ButtonText { get; set; } = "Read more...";

        public override bool ShouldRender(BlocklyListOptions listOptions) =>
            listOptions.Mode == BlocksListMode.Preview;

        public override bool ShouldRenderNext(BlocklyListOptions options) =>
            options.Mode == BlocksListMode.Full;
    }

    public record CutBlockDescriptor : BlockDescriptor<CutBlock>
    {
        public CutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
        {
        }
    }
}
