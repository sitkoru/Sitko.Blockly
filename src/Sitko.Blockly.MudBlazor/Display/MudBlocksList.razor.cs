using Microsoft.AspNetCore.Components;
using Sitko.Blazor.ScriptInjector;

namespace Sitko.Blockly.MudBlazorComponents.Display;

public partial class MudBlocksList
{
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ScriptInjector.InjectAsync(MudBlazorBlocklyModule.MudBlazorBlocklyCssRequest);
        }
    }
}
