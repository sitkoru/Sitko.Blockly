using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntQuoteBlockDescriptor : BlazorBlockDescriptor<QuoteBlock, AntQuoteBlockComponent, AntQuoteBlockForm>
    {
        public AntQuoteBlockDescriptor(ILocalizationProvider<QuoteBlock> localizationProvider) : base(
            localizationProvider)
        {
        }

        public override RenderFragment Icon => builder => builder.AddIcon("quote");
    }
}
