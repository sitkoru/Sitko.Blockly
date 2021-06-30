using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntTextBlockForm<TForm> where TForm : BaseForm, IBlocklyForm
    {
        [Inject] private ILocalizationProvider<AntTextBlockForm<TForm>> LocalizationProvider { get; set; } = null!;
    }

    
}
