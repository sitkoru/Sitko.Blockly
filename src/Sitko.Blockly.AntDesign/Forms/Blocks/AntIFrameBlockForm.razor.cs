using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntIFrameBlockForm<TForm> where TForm : BaseForm
    {
        [Inject] private ILocalizationProvider<AntIFrameBlockForm<TForm>> LocalizationProvider { get; set; } = null!;
    }
}
