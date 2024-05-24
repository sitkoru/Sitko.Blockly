using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.ScriptInjector;
using Sitko.Core.App;
using Sitko.EditorJS.Blazor.Display;

namespace Sitko.EditorJS.Blazor;

public abstract class BlazorEditorJSModule<TDescriptor, TConfig> : EditorJSModule<TDescriptor, TConfig>
    where TDescriptor : IBlazorBlockDescriptor
    where TConfig : BlazorEditorJSModuleOptions<TDescriptor>, new()
{
    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        TConfig startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        services.AddScriptInjector();
    }
}

public class BlazorEditorJSModuleOptions<TDescriptor> : EditorJSModuleOptions<TDescriptor>
    where TDescriptor : IBlazorBlockDescriptor
{
}
