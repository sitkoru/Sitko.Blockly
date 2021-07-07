using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Display.Blocks
{
    public partial class AntCutBlockComponent<TEntity>
    {
        [Inject] public ILocalizationProvider<AntCutBlockComponent<TEntity>> LocalizationProvider { get; set; } = null!;
    }
}
