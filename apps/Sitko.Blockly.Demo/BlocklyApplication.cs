using Serilog.Events;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.AntDesignComponents;
using Sitko.Blockly.Demo.Data;
using Sitko.Core.App.Blazor;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace Sitko.Blockly.Demo
{
    public class BlocklyApplication : BlazorApplication<Startup>
    {
        public BlocklyApplication(string[] args) : base(args)
        {
            AddModule<PostgresModule<BlocklyContext>, PostgresDatabaseModuleConfig<BlocklyContext>>();
            AddModule<EFRepositoriesModule<BlocklyContext>, EFRepositoriesModuleConfig>();
            AddModule<AntDesignBlocklyModule, AntDesignBlocklyModuleConfig>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.AddDefaultFluentValidators();
                    moduleConfig.AddDefaultBlocks();
                    moduleConfig.CKEditorTheme = CKEditorTheme.Dark;
                });
            ConfigureLogLevel("System.Net.Http.HttpClient.health-checks",
                LogEventLevel.Error).ConfigureLogLevel("Microsoft.AspNetCore.Components", LogEventLevel.Warning);
            ConfigureLogLevel("Microsoft.AspNetCore.SignalR", LogEventLevel.Warning);
            ConfigureLogLevel("Microsoft.EntityFrameworkCore.ChangeTracking", LogEventLevel.Warning);
        }
    }
}
