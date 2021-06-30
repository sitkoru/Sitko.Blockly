using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntFilesBlockForm
    {
        [Inject]
        private ILocalizationProvider<AntFilesBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
