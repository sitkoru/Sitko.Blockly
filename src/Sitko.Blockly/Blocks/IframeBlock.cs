using Microsoft.Extensions.Localization;

namespace Sitko.Blockly.Blocks
{
    public record IframeBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"Frame: {Src}";
        }

        public string Src { get; set; } = "";
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
    }
    
    public record IframeBlockDescriptor : BlockDescriptor<IframeBlock>
    {
        public IframeBlockDescriptor(IStringLocalizer<IframeBlock>? localizer = null) : base(localizer)
        {
        }
    }
}
