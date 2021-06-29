using Microsoft.Extensions.Localization;

namespace Sitko.Blockly.Blocks
{
    public record TextBlock : ContentBlock
    {
        public override string ToString()
        {
            return Text;
        }

        public string Text { get; set; } = "";
    }
    
    public record TextBlockDescriptor : BlockDescriptor<TextBlock>
    {
        public TextBlockDescriptor(IStringLocalizer<TextBlock>? localizer = null) : base(localizer)
        {
        }
    }
}
