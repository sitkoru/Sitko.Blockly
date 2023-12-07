using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace Sitko.Blockly;

[PublicAPI]
public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddBlockly(this IHostApplicationBuilder hostApplicationBuilder,
        Action<IApplicationContext, BlocklyModuleOptions> configure, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore().AddBlockly(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static IHostApplicationBuilder AddBlockly(this IHostApplicationBuilder hostApplicationBuilder,
        Action<BlocklyModuleOptions>? configure = null, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore().AddBlockly(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static ISitkoCoreApplicationBuilder AddBlockly(this ISitkoCoreApplicationBuilder applicationBuilder,
        Action<IApplicationContext, BlocklyModuleOptions> configure, string? configKey = null) =>
        applicationBuilder.AddModule<BlocklyModule, BlocklyModuleOptions>(configure, configKey);

    public static ISitkoCoreApplicationBuilder AddBlockly(this ISitkoCoreApplicationBuilder applicationBuilder,
        Action<BlocklyModuleOptions>? configure = null, string? configKey = null) =>
        applicationBuilder.AddModule<BlocklyModule, BlocklyModuleOptions>(configure, configKey);
}
