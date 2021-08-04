using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    using System.Threading.Tasks;
    using Sitko.Blazor.ScriptInjector;

    public partial class AntBlocklyForm<TEntity, TForm> where TEntity : class, new()
        where TForm : Sitko.Core.App.Blazor.Forms.BaseForm<TEntity>
    {
        [Inject]
        protected ILocalizationProvider<AntBlocklyForm<TEntity, TForm>> LocalizationProvider { get; set; } = null!;

        [Inject] protected IOptions<AntDesignBlocklyModuleOptions> ModuleOptions { get; set; } = null!;

        [Parameter] public string? Label { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await ScriptInjector.InjectAsync(AntDesignBlocklyModule.AntDesignBlocklyCssRequest);
            }
        }
    }
}
