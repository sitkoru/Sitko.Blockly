using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.Blazor;

namespace Sitko.Blockly.AntDesignComponents;

[PublicAPI]
public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddAntDesignBlockly(this IHostApplicationBuilder hostApplicationBuilder,
        Action<IApplicationContext, AntDesignBlocklyModuleOptions> configure, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreBlazorApplicationBuilder>()
            .AddAntDesignBlockly(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static IHostApplicationBuilder AddAntDesignBlockly(this IHostApplicationBuilder hostApplicationBuilder,
        Action<AntDesignBlocklyModuleOptions>? configure = null, string? optionsKey = null)
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreBlazorApplicationBuilder>()
            .AddAntDesignBlockly(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static ISitkoCoreBlazorApplicationBuilder AddAntDesignBlockly(
        this ISitkoCoreBlazorApplicationBuilder applicationBuilder,
        Action<IApplicationContext, AntDesignBlocklyModuleOptions> configure,
        string? configKey = null)
    {
        applicationBuilder
            .AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(configure, configKey);
        return applicationBuilder;
    }

    public static ISitkoCoreBlazorApplicationBuilder AddAntDesignBlockly(
        this ISitkoCoreBlazorApplicationBuilder applicationBuilder,
        Action<AntDesignBlocklyModuleOptions>? configure = null, string? configKey = null)
    {
        applicationBuilder
            .AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(configure, configKey);
        return applicationBuilder;
    }
}
