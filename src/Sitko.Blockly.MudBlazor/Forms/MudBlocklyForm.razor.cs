using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Forms;

public partial class MudBlocklyForm
{
    [Inject] protected ILocalizationProvider<MudBlocklyForm> LocalizationProvider { get; set; } = null!;

    [Inject] protected IOptions<MudBlazorBlocklyModuleOptions> ModuleOptions { get; set; } = null!;

    [Parameter] public string? Label { get; set; }

    private Guid? _openBlockId;
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ScriptInjector.InjectAsync(MudBlazorBlocklyModule.MudBlazorBlocklyCssRequest);
        }
    }
    private void ToggleOpen(Guid blockId)
    {
        if (_openBlockId == blockId)
        {
            _openBlockId = null;
        }
        else
        {
            _openBlockId = blockId;
        }
    }

}
