using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntTwitterBlockForm
    {
        [Inject] private ILocalizationProvider<AntTwitterBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
