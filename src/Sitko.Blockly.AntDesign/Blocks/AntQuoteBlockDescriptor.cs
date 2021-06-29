using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntQuoteBlockDescriptor : QuoteBlockDescriptor, IBlazorBlockDescriptor<QuoteBlock>
    {
        public AntQuoteBlockDescriptor(IStringLocalizer<QuoteBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("quote");
        public Type FormComponent => typeof(AntQuoteBlockForm<>);
        public Type DisplayComponent => typeof(AntQuoteBlockComponent<>);
    }
}
