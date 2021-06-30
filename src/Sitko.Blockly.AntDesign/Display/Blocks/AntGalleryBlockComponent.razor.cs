using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Display.Blocks
{
    public partial class AntGalleryBlockComponent<TEntity>
    {
        [Inject]
        public ILocalizationProvider<AntGalleryBlockComponent<TEntity>> LocalizationProvider { get; set; } = null!;
    }
}
