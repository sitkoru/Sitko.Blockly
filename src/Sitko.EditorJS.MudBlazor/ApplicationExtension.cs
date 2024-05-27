using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.Blazor;

namespace Sitko.EditorJS.MudBlazor;

public static class ApplicationExtension
{

    public static IHostApplicationBuilder AddMudBlazorEditorJS(this IHostApplicationBuilder hostApplicationBuilder,
        Action<IApplicationContext, MudBlazorEditorJSModuleOptions> configure, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreBlazorApplicationBuilder>()
            .AddMudBlazorEditorJS(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static IHostApplicationBuilder AddMudBlazorEditorJS(this IHostApplicationBuilder hostApplicationBuilder,
        Action<MudBlazorEditorJSModuleOptions>? configure = null, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreBlazorApplicationBuilder>()
            .AddMudBlazorEditorJS(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static ISitkoCoreBlazorApplicationBuilder AddMudBlazorEditorJS(
        this ISitkoCoreBlazorApplicationBuilder applicationBuilder,
        Action<IApplicationContext, MudBlazorEditorJSModuleOptions> configure,
        string? configKey = null)
    {
        applicationBuilder
            .AddModule<MudBlazorEditorJSModule, MudBlazorEditorJSModuleOptions>(configure, configKey);
        return applicationBuilder;
    }

    public static ISitkoCoreBlazorApplicationBuilder AddMudBlazorEditorJS(
        this ISitkoCoreBlazorApplicationBuilder applicationBuilder,
        Action<MudBlazorEditorJSModuleOptions>? configure = null, string? configKey = null)
    {
        applicationBuilder
            .AddModule<MudBlazorEditorJSModule, MudBlazorEditorJSModuleOptions>(configure, configKey);
        return applicationBuilder;
    }
}
