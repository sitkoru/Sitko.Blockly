using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.Blazor;

namespace Sitko.Blockly.MudBlazorComponents;

[PublicAPI]
public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddMudBlazorBlockly(this IHostApplicationBuilder hostApplicationBuilder,
        Action<IApplicationContext, MudBlazorBlocklyModuleOptions> configure, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreBlazorApplicationBuilder>()
            .AddMudBlazorBlockly(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static IHostApplicationBuilder AddMudBlazorBlockly(this IHostApplicationBuilder hostApplicationBuilder,
        Action<MudBlazorBlocklyModuleOptions>? configure = null, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreBlazorApplicationBuilder>()
            .AddMudBlazorBlockly(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static ISitkoCoreBlazorApplicationBuilder AddMudBlazorBlockly(
        this ISitkoCoreBlazorApplicationBuilder applicationBuilder,
        Action<IApplicationContext, MudBlazorBlocklyModuleOptions> configure,
        string? configKey = null)
    {
        applicationBuilder
            .AddModule<MudBlazorBlocklyModule, MudBlazorBlocklyModuleOptions>(configure, configKey);
        return applicationBuilder;
    }

    public static ISitkoCoreBlazorApplicationBuilder AddMudBlazorBlockly(
        this ISitkoCoreBlazorApplicationBuilder applicationBuilder,
        Action<MudBlazorBlocklyModuleOptions>? configure = null, string? configKey = null)
    {
        applicationBuilder
            .AddModule<MudBlazorBlocklyModule, MudBlazorBlocklyModuleOptions>(configure, configKey);
        return applicationBuilder;
    }
}
