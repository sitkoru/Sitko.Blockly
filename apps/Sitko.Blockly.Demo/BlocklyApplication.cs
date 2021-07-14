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
            this.AddPostgresDatabase<BlocklyContext>()
                .AddEFRepositories<BlocklyContext>()
                .AddFileSystemStorage<BlocklyStorageOptions>()
                .AddPostgresStorageMetadata<BlocklyStorageOptions>()
                .AddJsonLocalization()
                .AddAntDesignBlockly(moduleOptions =>
                {
                    moduleOptions.AddBlocks<AntDesignBlocklyModule>();
                });
            ConfigureLogLevel("System.Net.Http.HttpClient.health-checks", LogEventLevel.Error)
                .ConfigureLogLevel("Microsoft.AspNetCore", LogEventLevel.Warning)
                .ConfigureLogLevel("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
        }

        protected override bool LoggingEnableConsole(HostBuilderContext context) => true;
    }

    public class BlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
    {
        public string StoragePath { get; set; } = "";
    }

    public record TestMetadata(Guid Id, string Type);
}
