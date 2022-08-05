using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor;
using Sitko.Core.App;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents;

public class AntDesignBlocklyModule : BlazorBlocklyModule<IBlazorBlockDescriptor, AntDesignBlocklyModuleOptions>
{
    public static readonly CssInjectRequest AntDesignBlocklyCssRequest = CssInjectRequest.FromUrl(
        "blocklyAnt", "/_content/Sitko.Blockly.AntDesign/Sitko.Blockly.AntDesign.bundle.scp.css");

    public override string OptionsKey => "Blockly:AntDesign";

    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        AntDesignBlocklyModuleOptions startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        services.AddCKEditorBundle(context.Configuration);
        services.Configure<JsonLocalizationModuleOptions>(options =>
        {
            options.AddDefaultResource<AntDesignBlocklyModule>();
        });
    }

    public override async Task InitAsync(IApplicationContext context, IServiceProvider serviceProvider)
    {
        await base.InitAsync(context, serviceProvider);
        var options = GetOptions(serviceProvider);
        options.IconSources.Add(typeof(CustomIconsProvider).GetTypeInfo().Assembly,
            "Sitko.Blockly.AntDesignComponents.Icons.");
        await CustomIconsProvider.InitAsync(options.IconSources);
    }
}
