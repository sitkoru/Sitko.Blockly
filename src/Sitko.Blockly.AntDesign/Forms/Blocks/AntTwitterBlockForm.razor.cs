using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntTwitterBlockForm
    {
        [Inject] protected ILocalizationProvider<AntTwitterBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
