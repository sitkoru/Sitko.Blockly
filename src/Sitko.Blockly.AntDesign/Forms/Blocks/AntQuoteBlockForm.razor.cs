using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntQuoteBlockForm
    {
        [Inject] private ILocalizationProvider<AntQuoteBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
