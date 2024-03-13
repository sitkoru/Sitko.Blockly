using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record MudQuoteBlockDescriptor : BlazorBlockDescriptor<QuoteBlock, MudQuoteBlockComponent, MudQuoteBlockForm>
{
    public MudQuoteBlockDescriptor(ILocalizationProvider<QuoteBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => Icons.Filled.FormatQuote;
}
