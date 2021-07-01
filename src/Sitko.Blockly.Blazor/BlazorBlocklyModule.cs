using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Blockly.Blazor.Forms;
using Sitko.Core.App;

namespace Sitko.Blockly.Blazor
{
    public abstract class BlazorBlocklyModule<TDescriptor, TConfig> : BlocklyModule<TDescriptor, TConfig>
        where TDescriptor : IBlazorBlockDescriptor
        where TConfig : BlazorBlocklyModuleConfig<TDescriptor>, new()
    {
        protected BlazorBlocklyModule(TConfig config, Application application) : base(config,
            application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            services.AddScoped<BlocklyFormService>();
        }
    }
}
