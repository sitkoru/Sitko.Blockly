using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.EditorJS.Blazor;

namespace Sitko.Blockly;

[PublicAPI]
public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddEditorJS(this IHostApplicationBuilder hostApplicationBuilder,
        Action<IApplicationContext, EditorJSModuleOptions> configure, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore().AddEditorJS(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static IHostApplicationBuilder AddEditorJS(this IHostApplicationBuilder hostApplicationBuilder,
        Action<EditorJSModuleOptions>? configure = null, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore().AddEditorJS(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static ISitkoCoreApplicationBuilder AddEditorJS(this ISitkoCoreApplicationBuilder applicationBuilder,
        Action<IApplicationContext, EditorJSModuleOptions> configure, string? configKey = null) =>
        applicationBuilder.AddModule<EditorJSModule, EditorJSModuleOptions>(configure, configKey);

    public static ISitkoCoreApplicationBuilder AddEditorJS(this ISitkoCoreApplicationBuilder applicationBuilder,
        Action<EditorJSModuleOptions>? configure = null, string? configKey = null) =>
        applicationBuilder.AddModule<EditorJSModule, EditorJSModuleOptions>(configure, configKey);
}
