using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntQuoteBlockDescriptor : QuoteBlockDescriptor, IBlazorBlockDescriptor<QuoteBlock>
    {
        public AntQuoteBlockDescriptor(ILocalizationProvider<QuoteBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("quote");
        public virtual Type FormComponent => typeof(AntQuoteBlockForm);
        public virtual Type DisplayComponent => typeof(AntQuoteBlockComponent<>);
    }
}
