using System;
using Serilog.Events;
using Sitko.Blockly.Demo.Data;
using Sitko.Blockly.MudBlazorComponents;
using Sitko.Core.App.Localization;
using Sitko.Core.Blazor.MudBlazor.Server;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Storage;
using Sitko.Core.Storage.FileSystem;
using Sitko.Core.Storage.Metadata.Postgres;

namespace Sitko.Blockly.Demo;

public class BlocklyApplication : MudBlazorApplication<Startup>
{
    public BlocklyApplication(string[] args) : base(args)
    {
        this.AddPostgresDatabase<BlocklyContext>()
            .AddEFRepositories<BlocklyContext>()
            .AddFileSystemStorage<BlocklyStorageOptions>()
            .AddPostgresStorageMetadata<BlocklyStorageOptions>()
            .AddJsonLocalization()
            .AddMudBlazorBlockly(moduleOptions =>
            {
                moduleOptions.AddBlocks<MudBlazorBlocklyModule>();
            });
        ConfigureLogLevel("System.Net.Http.HttpClient.health-checks", LogEventLevel.Error)
            .ConfigureLogLevel("Microsoft.AspNetCore", LogEventLevel.Warning)
            .ConfigureLogLevel("Microsoft.EntityFrameworkCore", LogEventLevel.Warning);
    }
}

public class BlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
{
    public string StoragePath { get; set; } = "";
}

public record TestMetadata(Guid Id, string Type);
