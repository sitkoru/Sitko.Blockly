using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntIFrameBlockForm
    {
        [Inject] protected ILocalizationProvider<AntIFrameBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
