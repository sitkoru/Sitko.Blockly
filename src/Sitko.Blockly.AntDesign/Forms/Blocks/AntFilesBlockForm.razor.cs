using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntFilesBlockForm<TForm> where TForm : BaseForm
    {
        [Inject]
        private ILocalizationProvider<AntFilesBlockForm<TForm>> LocalizationProvider { get; set; } = null!;
    }
}
