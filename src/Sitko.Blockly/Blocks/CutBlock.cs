using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks
{
    public record CutBlock : ContentBlock

    {
        public override string ToString() => "";

        public string ButtonText { get; set; } = "Read more...";
    }

    public record CutBlockDescriptor : BlockDescriptor<CutBlock>
    {
        public CutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
        {
        }
        public override int Priority => 2;
        public override int MaxCount => 1;
    }
}
