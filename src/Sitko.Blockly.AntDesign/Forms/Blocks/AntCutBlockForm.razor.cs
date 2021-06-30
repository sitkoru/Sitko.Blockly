using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntCutBlockForm<TForm> where TForm : BaseForm, IBlocklyForm
    {
        [Inject]
        private ILocalizationProvider<AntCutBlockForm<TForm>> LocalizationProvider { get; set; } = null!;
    }
}
