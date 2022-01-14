using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor.Forms;
using Sitko.Core.App;

namespace Sitko.Blockly.Blazor;

public abstract class BlazorBlocklyModule<TDescriptor, TConfig> : BlocklyModule<TDescriptor, TConfig>
    where TDescriptor : IBlazorBlockDescriptor
    where TConfig : BlazorBlocklyModuleOptions<TDescriptor>, new()
{
    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        TConfig startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        services.AddScoped<BlocklyFormService>();
        services.AddScriptInjector();
    }
}
