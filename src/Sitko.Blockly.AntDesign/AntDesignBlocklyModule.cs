using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.Blazor;
using Sitko.Core.App;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents
{
    using Sitko.Blazor.ScriptInjector;

    public class AntDesignBlocklyModule : BlazorBlocklyModule<IBlazorBlockDescriptor, AntDesignBlocklyModuleOptions>
    {
        public static readonly CssInjectRequest AntDesignBlocklyCssRequest = CssInjectRequest.FromUrl(
            "blocklyAnt", "/_content/Sitko.Blockly.AntDesign/Sitko.Blockly.AntDesign.bundle.scp.css");

        public override void ConfigureServices(ApplicationContext context, IServiceCollection services,
            AntDesignBlocklyModuleOptions startupOptions)
        {
            base.ConfigureServices(context, services, startupOptions);
            services.AddCKEditorBundle(context.Configuration);
            services.Configure<JsonLocalizationModuleOptions>(options =>
            {
                options.AddDefaultResource<AntDesignBlocklyModule>();
            });
        }

        public override string OptionsKey => "Blockly:AntDesign";

        public override async Task InitAsync(ApplicationContext context, IServiceProvider serviceProvider)
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
                        string text = await reader.ReadToEndAsync(); //hello world!
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
}
