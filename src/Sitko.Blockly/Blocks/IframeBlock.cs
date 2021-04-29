using System;

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
}
