using System;
using Microsoft.Extensions.Localization;

namespace Sitko.Blockly.Blocks
{
    public record CutBlock : ContentBlock

    {
        public override string ToString()
        {
            return "";
        }

        public string ButtonText { get; set; } = "Read more...";
    }

    public record CutBlockDescriptor : BlockDescriptor<CutBlock>
    {
        public CutBlockDescriptor(IStringLocalizer<CutBlock>? localizer = null) : base(localizer)
        {
        }
    }
}
