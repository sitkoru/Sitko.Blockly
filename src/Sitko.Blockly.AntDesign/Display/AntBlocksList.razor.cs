using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Sitko.Blazor.ScriptInjector;

namespace Sitko.Blockly.AntDesignComponents.Display;

public partial class AntBlocksList
{
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ScriptInjector.InjectAsync(AntDesignBlocklyModule.AntDesignBlocklyCssRequest);
        }
    }
}
