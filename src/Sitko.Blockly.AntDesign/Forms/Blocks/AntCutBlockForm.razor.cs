using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntCutBlockForm
    {
        [Inject] private ILocalizationProvider<AntCutBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
