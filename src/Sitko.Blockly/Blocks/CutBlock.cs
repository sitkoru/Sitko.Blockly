using Sitko.Blockly.Display;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks;

[ContentBlockMetadata(2, 1)]
public record CutBlock : ContentBlock

{
    public string ButtonText { get; set; } = "Read more...";
    public override string ToString() => "";

    public override bool ShouldRender(BlocklyListOptions listOptions) =>
        listOptions.Mode == BlocksListMode.Preview;

    public override bool ShouldRenderNext(BlocklyListOptions listOptions) =>
        listOptions.Mode == BlocksListMode.Full;
}

public record CutBlockDescriptor : BlockDescriptor<CutBlock>
{
    public CutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
