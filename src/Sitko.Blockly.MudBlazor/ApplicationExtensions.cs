using JetBrains.Annotations;
using Sitko.Core.App;

namespace Sitko.Blockly.MudBlazorComponents;

[PublicAPI]
public static class ApplicationExtensions
{
    public static Application AddMudBlazorBlockly(this Application application,
        Action<IApplicationContext, MudBlazorBlocklyModuleOptions> configure,
        string? configKey = null) =>
        application.AddModule<MudBlazorBlocklyModule, MudBlazorBlocklyModuleOptions>(configure, configKey);

    public static Application AddMudBlazorBlockly(this Application application,
        Action<MudBlazorBlocklyModuleOptions>? configure = null, string? configKey = null) =>
        application.AddModule<MudBlazorBlocklyModule, MudBlazorBlocklyModuleOptions>(configure, configKey);
}
