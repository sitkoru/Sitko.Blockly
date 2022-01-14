using JetBrains.Annotations;
using Sitko.Core.App;

namespace Sitko.Blockly;

[PublicAPI]
public static class ApplicationExtensions
{
    public static Application AddBlockly(this Application application,
        Action<IApplicationContext, BlocklyModuleOptions> configure, string? configKey = null) =>
        application.AddModule<BlocklyModule, BlocklyModuleOptions>(configure, configKey);

    public static Application AddBlockly(this Application application,
        Action<BlocklyModuleOptions>? configure = null, string? configKey = null) =>
        application.AddModule<BlocklyModule, BlocklyModuleOptions>(configure, configKey);
}
