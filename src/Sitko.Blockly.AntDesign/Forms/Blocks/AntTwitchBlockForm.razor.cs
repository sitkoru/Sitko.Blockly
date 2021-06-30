using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntTwitchBlockForm<TForm> where TForm : BaseForm
    {
        [Inject] private ILocalizationProvider<AntTwitchBlockForm<TForm>> LocalizationProvider { get; set; } = null!;
    }
}
