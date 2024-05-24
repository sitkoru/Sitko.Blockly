using Microsoft.Extensions.DependencyInjection;
using Sitko.Core.App;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor;
using Sitko.EditorJS.Blazor.Display;

namespace Sitko.EditorJS.MudBlazor;

public class MudBlazorEditorJSModule : BlazorEditorJSModule<IBlazorBlockDescriptor, MudBlazorEditorJSModuleOptions>
{
    public static readonly string CssUrl = "/_content/Sitko.EditorJS.MudBlazor/Sitko.EditorJS.MudBlazor.bundle.scp.css";

    public override string OptionsKey => "EditorJS:MudBlazor";

    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        MudBlazorEditorJSModuleOptions startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        services.Configure<JsonLocalizationModuleOptions>(options =>
        {
            options.AddDefaultResource<MudBlazorEditorJSModule>();
        });
    }
}
public class MudBlazorEditorJSModuleOptions : BlazorEditorJSModuleOptions<IBlazorBlockDescriptor>
{
    public MudBlazorEditorJSTheme Theme { get; set; } = MudBlazorEditorJSTheme.Light;
}

public enum MudBlazorEditorJSTheme
{
    Light,
    Dark
}
