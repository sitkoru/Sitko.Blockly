using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntQuoteBlockForm
    {
        [Inject] protected ILocalizationProvider<AntQuoteBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
