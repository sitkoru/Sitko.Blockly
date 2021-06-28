using System;
using System.Threading.Tasks;
using Amazon;
using Npgsql;
using Serilog.Events;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.AntDesignComponents;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Demo.Data;
using Sitko.Blockly.Demo.Pages;
using Sitko.Core.Blazor.AntDesignComponents;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Storage;
using Sitko.Core.Storage.Metadata.Postgres;
using Sitko.Core.Storage.S3;

namespace Sitko.Blockly.Demo
{
    public class BlocklyApplication : AntBlazorApplication<Startup>
    {
        public BlocklyApplication(string[] args) : base(args)
        {
            AddModule<PostgresModule<BlocklyContext>, PostgresDatabaseModuleConfig<BlocklyContext>>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.Database = "Blockly";
                });
            AddModule<EFRepositoriesModule<BlocklyContext>, EFRepositoriesModuleConfig>();
            AddModule<S3StorageModule<BlocklyStorageOptions>, BlocklyStorageOptions>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.PublicUri = new Uri("http://localhost:9000/blockly/");
                    moduleConfig.Server = new Uri("http://localhost:9000");
                    moduleConfig.Bucket = "blockly";
                    moduleConfig.AccessKey = "A8eENTqgEE7uYL7R";
                    moduleConfig.SecretKey = "82bmVoDRkZgwy4B3PXkLzpXiuqGVZMug";
                    moduleConfig
                        .UseMetadata<PostgresStorageMetadataProvider<BlocklyStorageOptions>,
                            PostgresStorageMetadataProviderOptions>(options =>
                        {
                            var builder = new NpgsqlConnectionStringBuilder
                            {
                                Host = "localhost",
                                Username = "postgres",
                                Password = "123",
                                Database = "Blockly",
                                SearchPath = "storage,public"
                            };
                            options.ConnectionString = builder.ConnectionString;
                            options.Schema = "storage";
                        });
                });
            AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleConfig>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.AddDefaultFluentValidators();
                    moduleConfig.AddDefaultBlocks();
                    moduleConfig.CKEditorTheme = CKEditorTheme.Dark;
                    moduleConfig.ConfigureDefaultStorage<DemoBlockFormStorageOptions>();
                    moduleConfig.ConfigureFormStorage<PostForm, PostDemoBlockFormStorageOptions>();
                });
            ConfigureLogLevel("System.Net.Http.HttpClient.health-checks",
                LogEventLevel.Error).ConfigureLogLevel("Microsoft.AspNetCore.Components", LogEventLevel.Warning);
            ConfigureLogLevel("Microsoft.AspNetCore.SignalR", LogEventLevel.Warning);
            ConfigureLogLevel("Microsoft.EntityFrameworkCore.ChangeTracking", LogEventLevel.Warning);
        }
    }

    public class DemoBlockFormStorageOptions : BlockFormStorageOptions<BlocklyStorageOptions>
    {
        public DemoBlockFormStorageOptions(IStorage<BlocklyStorageOptions> storage) : base(storage)
        {
            ImagesUploadPath = "images";
            FilesUploadPath = "images";
            MaxAllowedImages = 10;
            MaxAllowedFiles = 10;
            MaxFileSize = 100 * 1024 * 1024; // 100Mb
            MaxImageSize = 2 * 1024 * 1024; // 2Mb
            GenerateMetadata = (_, _) =>
            {
                var metadata = new TestMetadata(Guid.NewGuid(), "File");
                return Task.FromResult<object>(metadata);
            };
            GenerateImageMetadata = (_, _) =>
            {
                var metadata = new TestMetadata(Guid.NewGuid(), "Image");
                return Task.FromResult<object>(metadata);
            };
        }
    }

    public class PostDemoBlockFormStorageOptions : DemoBlockFormStorageOptions, IBlockFormStorageOptions<PostForm>
    {
        public PostDemoBlockFormStorageOptions(IStorage<BlocklyStorageOptions> storage) : base(storage)
        {
            ImagesUploadPath = "posts/images";
            FilesUploadPath = "posts/files";
        }
    }

    public class BlocklyStorageOptions : StorageOptions, IS3StorageOptions
    {
        public override string Name { get; set; } = "Blockly";
        public Uri Server { get; set; }
        public string Bucket { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public RegionEndpoint Region { get; set; } = RegionEndpoint.USEast1;
    }

    public record TestMetadata(Guid Id, string Type);
}
