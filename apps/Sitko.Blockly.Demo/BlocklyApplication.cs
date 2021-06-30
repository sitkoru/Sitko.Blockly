using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Serilog.Events;
using Sitko.Blockly.AntDesignComponents;
using Sitko.Blockly.Demo.Data;
using Sitko.Core.App;
using Sitko.Core.App.Localization;
using Sitko.Core.Blazor.AntDesignComponents;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Storage;
using Sitko.Core.Storage.Metadata.Postgres;
using Sitko.Core.Storage.FileSystem;

namespace Sitko.Blockly.Demo
{
    public class BlocklyApplication : AntBlazorApplication<Startup>
    {
        public BlocklyApplication(string[] args) : base(args)
        {
            AddModule<PostgresModule<BlocklyContext>, PostgresDatabaseModuleConfig<BlocklyContext>>(
                (configuration, environment, moduleConfig) =>
                {
                    var builder = GetConnectionStringBuilder(configuration, environment);
                    moduleConfig.Host = builder.Host!;
                    moduleConfig.Port = builder.Port;
                    moduleConfig.Username = builder.Username!;
                    moduleConfig.Password = builder.Password!;
                    moduleConfig.Database = builder.Database!;
                    moduleConfig.EnableContextPooling = true;
                });
            AddModule<EFRepositoriesModule<BlocklyContext>, EFRepositoriesModuleConfig>();
            AddModule<FileSystemStorageModule<BlocklyStorageOptions>, BlocklyStorageOptions>(
                (configuration, environment, moduleConfig) =>
                {
                    moduleConfig.PublicUri = !string.IsNullOrEmpty(configuration["STORAGE_PUBLIC_URI"])
                        ? new Uri(configuration["STORAGE_PUBLIC_URI"])
                        : new Uri("http://localhost");
                    moduleConfig.StoragePath = Path.Combine(Path.GetFullPath("wwwroot"), "static");
                    moduleConfig
                        .UseMetadata<PostgresStorageMetadataProvider<BlocklyStorageOptions>,
                            PostgresStorageMetadataProviderOptions>(options =>
                        {
                            var builder = GetConnectionStringBuilder(configuration, environment);
                            builder.SearchPath = "storage,public";
                            options.ConnectionString = builder.ConnectionString;
                            options.Schema = "storage";
                        });
                });
            AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleConfig>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.AddBlocks<AntDesignBlocklyModule>();
                    moduleConfig.Theme = AntDesignBlocklyTheme.Dark;
                });
            ConfigureLogLevel("System.Net.Http.HttpClient.health-checks", LogEventLevel.Error)
                .ConfigureLogLevel("Microsoft.AspNetCore", LogEventLevel.Warning)
                .ConfigureLogLevel("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
            this.ConfigureServices(services =>
            {
                services.AddJsonLocalization();
            });
        }

        protected override bool LoggingEnableConsole => true;

        private NpgsqlConnectionStringBuilder GetConnectionStringBuilder(IConfiguration configuration,
            IHostEnvironment environment)
        {
            var connBuilder = new NpgsqlConnectionStringBuilder
            {
                Host = configuration["POSTGRES_HOST"] ?? "localhost",
                Port = !string.IsNullOrEmpty(configuration["POSTGRES_PORT"])
                    ? int.Parse(configuration["POSTGRES_PORT"])
                    : 5432,
                Username = configuration["POSTGRES_USERNAME"] ?? "postgres",
                Password = configuration["POSTGRES_PASSWORD"] ?? "",
                Database = configuration["POSTGRES_DATABASE"] ?? "Blockly",
                Pooling = !environment.IsProduction()
            };
            return connBuilder;
        }
    }

    public class BlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
    {
        public override string Name { get; set; } = "Blockly";
        public string StoragePath { get; set; } = "";
    }

    public record TestMetadata(Guid Id, string Type);
}
