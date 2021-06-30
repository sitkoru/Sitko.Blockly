using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntYoutubeBlockForm
    {
        [Inject] private ILocalizationProvider<AntYoutubeBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
