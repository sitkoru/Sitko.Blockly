using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace Sitko.Blockly;

[PublicAPI]
public static class ApplicationExtensions
{
    public static Application AddBlockly(this Application application,
        Action<IConfiguration, IHostEnvironment, BlocklyModuleOptions> configure, string? configKey = null) =>
        application.AddModule<BlocklyModule, BlocklyModuleOptions>(configure, configKey);

    public static Application AddBlockly(this Application application,
        Action<BlocklyModuleOptions>? configure = null, string? configKey = null) =>
        application.AddModule<BlocklyModule, BlocklyModuleOptions>(configure, configKey);
}
