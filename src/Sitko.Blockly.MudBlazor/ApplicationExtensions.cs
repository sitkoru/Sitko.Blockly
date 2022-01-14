using JetBrains.Annotations;
using Sitko.Core.App;

namespace Sitko.Blockly.MudBlazor;

[PublicAPI]
public static class ApplicationExtensions
{
    public static Application AddAntDesignBlockly(this Application application,
        Action<IApplicationContext, MudBlazorBlocklyModuleOptions> configure,
        string? configKey = null) =>
        application.AddModule<MudBlazorBlocklyModule, MudBlazorBlocklyModuleOptions>(configure, configKey);

    public static Application AddAntDesignBlockly(this Application application,
        Action<MudBlazorBlocklyModuleOptions>? configure = null, string? configKey = null) =>
        application.AddModule<MudBlazorBlocklyModule, MudBlazorBlocklyModuleOptions>(configure, configKey);
}
