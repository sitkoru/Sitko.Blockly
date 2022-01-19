using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using Sitko.Core.Blazor.MudBlazor.Server;

namespace Sitko.Blockly.Demo;

public class Startup : MudBlazorStartup
{
    public Startup(IConfiguration configuration, IHostEnvironment environment) : base(configuration, environment)
    {
    }

    protected override void ConfigureAppServices(IServiceCollection services)
    {
        base.ConfigureAppServices(services);
        services.AddValidatorsFromAssemblyContaining<Startup>();
        services.AddMudServices();
        services.AddServerSideBlazor().AddCircuitOptions(options => {  options.DetailedErrors = true; });

    }

    protected override void ConfigureAfterRoutingMiddleware(IApplicationBuilder app)
    {
        base.ConfigureAfterRoutingMiddleware(app);
        app.UseRequestLocalization(new RequestLocalizationOptions()
            .AddSupportedCultures("en-US", "ru")
            .AddSupportedUICultures("en-US", "ru"));
    }
}
