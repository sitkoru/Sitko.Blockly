using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    using System.Threading.Tasks;

    public partial class AntBlocklyForm
    {
        [Inject] protected ILocalizationProvider<AntBlocklyForm> LocalizationProvider { get; set; } = null!;

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
