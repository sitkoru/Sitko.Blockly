using System.Collections;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    public partial class BlocklyForm<TEntity, TForm> where TEntity : class
        where TForm : Sitko.Core.App.Blazor.Forms.BaseForm<TEntity>
    {
        [Inject] protected ILocalizationProvider<AntDesignBlocklyModule> LocalizationProvider { get; set; } = null!;
    }
}
