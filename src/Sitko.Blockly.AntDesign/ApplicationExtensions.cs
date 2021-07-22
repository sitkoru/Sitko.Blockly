using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;

namespace Sitko.Blockly.AntDesignComponents
{
    using JetBrains.Annotations;

    [PublicAPI]
    public static class ApplicationExtensions
    {
        public static Application AddAntDesignBlockly(this Application application,
            Action<IConfiguration, IHostEnvironment, AntDesignBlocklyModuleOptions> configure,
            string? configKey = null) =>
            application.AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(configure, configKey);

        public static Application AddAntDesignBlockly(this Application application,
            Action<AntDesignBlocklyModuleOptions>? configure = null, string? configKey = null) =>
            application.AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(configure, configKey);
    }
}
