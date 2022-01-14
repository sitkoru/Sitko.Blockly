using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazor.Display.Blocks;
using Sitko.Blockly.MudBlazor.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazor.Blocks;

public record MudQuoteBlockDescriptor : BlazorBlockDescriptor<QuoteBlock, MudQuoteBlockComponent, MudQuoteBlockForm>
{
    public MudQuoteBlockDescriptor(ILocalizationProvider<QuoteBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon("quote");
}
