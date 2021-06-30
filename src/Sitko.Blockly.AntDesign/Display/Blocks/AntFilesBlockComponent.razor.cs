using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Display.Blocks
{
    public partial class AntFilesBlockComponent<TEntity>
    {
        [Inject]
        public ILocalizationProvider<AntFilesBlockComponent<TEntity>> LocalizationProvider { get; set; } = null!;
    }
}
