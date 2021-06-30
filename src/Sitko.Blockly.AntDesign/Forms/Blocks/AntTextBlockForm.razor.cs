using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntTextBlockForm
    {
        [Inject] private ILocalizationProvider<AntTextBlockForm> LocalizationProvider { get; set; } = null!;
    }

    
}
