using JetBrains.Annotations;
using Sitko.Core.App;

namespace Sitko.Blockly.AntDesignComponents;

[PublicAPI]
public static class ApplicationExtensions
{
    public static Application AddAntDesignBlockly(this Application application,
        Action<IApplicationContext, AntDesignBlocklyModuleOptions> configure,
        string? configKey = null) =>
        application.AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(configure, configKey);

    public static Application AddAntDesignBlockly(this Application application,
        Action<AntDesignBlocklyModuleOptions>? configure = null, string? configKey = null) =>
        application.AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(configure, configKey);
}
