using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public partial class AntGalleryBlockForm
    {
        [Inject] private ILocalizationProvider<AntGalleryBlockForm> LocalizationProvider { get; set; } = null!;
    }
}
