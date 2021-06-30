using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntTwitchBlockForm
    {
        [Inject] private ILocalizationProvider<AntTwitchBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
