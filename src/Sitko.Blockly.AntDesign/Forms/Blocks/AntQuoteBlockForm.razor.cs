using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntQuoteBlockForm<TForm> where TForm : BaseForm, IBlocklyForm
    {
        [Inject] private ILocalizationProvider<AntQuoteBlockForm<TForm>> LocalizationProvider { get; set; } = null!;
    }
}
