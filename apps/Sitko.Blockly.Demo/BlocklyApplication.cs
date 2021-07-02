using System;
using Microsoft.Extensions.Hosting;
using Serilog.Events;
using Sitko.Blockly.AntDesignComponents;
using Sitko.Blockly.Demo.Data;
using Sitko.Core.App.Localization;
using Sitko.Core.Blazor.AntDesignComponents;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Storage;
using Sitko.Core.Storage.FileSystem;
using Sitko.Core.Storage.Metadata.Postgres;

namespace Sitko.Blockly.Demo
{
    public class BlocklyApplication : AntBlazorApplication<Startup>
    {
        public BlocklyApplication(string[] args) : base(args)
        {
            AddModule<PostgresModule<BlocklyContext>, PostgresDatabaseModuleOptions<BlocklyContext>>();
            AddModule<EFRepositoriesModule<BlocklyContext>, EFRepositoriesModuleOptions>();
            AddModule<FileSystemStorageModule<BlocklyStorageOptions>, BlocklyStorageOptions>();
            AddModule<PostgresStorageMetadataModule<BlocklyStorageOptions>, PostgresStorageMetadataProviderOptions>();
            AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleOptions>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.AddBlocks<AntDesignBlocklyModule>();
                });
            ConfigureLogLevel("System.Net.Http.HttpClient.health-checks", LogEventLevel.Error)
                .ConfigureLogLevel("Microsoft.AspNetCore", LogEventLevel.Warning)
                .ConfigureLogLevel("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
            AddModule<JsonLocalizationModule, JsonLocalizationModuleOptions>();
        }

        protected override bool LoggingEnableConsole(HostBuilderContext context)
        {
            return true;
        }
    }

    public class BlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
    {
        public override string Name { get; set; } = "Blockly";
        public string StoragePath { get; set; } = "";
    }

    public record TestMetadata(Guid Id, string Type);
}
