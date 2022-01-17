﻿using System.Reflection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor;
using Sitko.Core.App;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazor;

public class MudBlazorBlocklyModule : BlazorBlocklyModule<IBlazorBlockDescriptor, MudBlazorBlocklyModuleOptions>
{
     public static readonly CssInjectRequest MudBlazorBlocklyCssRequest = CssInjectRequest.FromUrl(
         "blocklyAnt", "/_content/Sitko.Blockly.AntDesign/Sitko.Blockly.AntDesign.bundle.scp.css");

    public override string OptionsKey => "Blockly:MudBlazor";

    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        MudBlazorBlocklyModuleOptions startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        services.AddCKEditorBundle(context.Configuration);
        services.Configure<JsonLocalizationModuleOptions>(options =>
        {
            options.AddDefaultResource<MudBlazorBlocklyModule>();
        });
    }

    public override async Task InitAsync(IApplicationContext context, IServiceProvider serviceProvider)
    {
        await base.InitAsync(context, serviceProvider);
        await CustomIconsProvider.InitAsync();
    }
}

public static class CustomIconsProvider
{
    private static readonly Dictionary<string, string> Icons = new();

    public static async Task InitAsync()
    {
        var assembly = typeof(CustomIconsProvider).GetTypeInfo().Assembly;
        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            if (resourceName.EndsWith(".svg", StringComparison.InvariantCulture))
            {
                var resource = assembly.GetManifestResourceStream(resourceName);
                if (resource is not null)
                {
                    StreamReader reader = new(resource);
                    var text = await reader.ReadToEndAsync();
                    var name = resourceName.Replace("Sitko.Blockly.AntDesignComponents.Icons.", "")
                        .Replace(".svg", "");
                    Icons.Add(name, text);
                }
            }
        }
    }

    public static RenderFragment GetIcon(string iconName) => builder =>
    {
        if (Icons.ContainsKey(iconName))
        {
            builder.AddMarkupContent(1, Icons[iconName]);
        }
    };
}