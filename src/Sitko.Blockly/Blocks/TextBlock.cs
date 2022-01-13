using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks;

[ContentBlockMetadata(1)]
public record TextBlock : ContentBlock
{
    public string Text { get; set; } = "";
    public override string ToString() => Text;
}

public record TextBlockDescriptor : BlockDescriptor<TextBlock>
{
    public TextBlockDescriptor(ILocalizationProvider<TextBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
