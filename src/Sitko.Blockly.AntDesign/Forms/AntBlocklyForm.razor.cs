using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    public partial class AntBlocklyForm<TEntity, TForm> where TEntity : class
        where TForm : Sitko.Core.App.Blazor.Forms.BaseForm<TEntity>
    {
        [Inject]
        protected ILocalizationProvider<AntBlocklyForm<TEntity, TForm>> LocalizationProvider { get; set; } = null!;

        [Inject] protected IOptions<AntDesignBlocklyModuleOptions> ModuleOptions { get; set; } = null!;

        [Parameter] public string? Label { get; set; }
    }
}
