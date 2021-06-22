using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App.Blazor;
using Sitko.Core.Blazor.AntDesignComponents;

namespace Sitko.Blockly.Demo
{
    public class Startup : AntBlazorStartup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
        {
        }

        protected override void ConfigureAppServices(IServiceCollection services)
        {
            base.ConfigureAppServices(services);
            services.AddValidatorsFromAssemblyContaining<Startup>();
        }
    }
}
