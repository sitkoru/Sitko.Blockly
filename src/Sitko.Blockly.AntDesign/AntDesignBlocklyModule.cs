using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.Blazor;
using Sitko.Core.App;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents
{
    public class AntDesignBlocklyModule : BlazorBlocklyModule<IBlazorBlockDescriptor, AntDesignBlocklyModuleConfig>
    {
        public AntDesignBlocklyModule(AntDesignBlocklyModuleConfig config, Application application) :
            base(config,
                application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);

            services.AddCKEditorBundle(configuration, Config.Theme switch
            {
                AntDesignBlocklyTheme.Light => CKEditorTheme.Light,
                AntDesignBlocklyTheme.Dark => CKEditorTheme.Dark,
                _ => throw new ArgumentOutOfRangeException()
            });

            services.Configure<JsonStringLocalizerOptions>(options =>
            {
                options.AddDefaultResource<AntDesignBlocklyModule>();
            });
        }

        public override async Task InitAsync(IServiceProvider serviceProvider, IConfiguration configuration,
            IHostEnvironment environment)
        {
            await base.InitAsync(serviceProvider, configuration, environment);
            await CustomIconsProvider.InitAsync();
        }
    }

    public static class CustomIconsProvider
    {
        private static readonly Dictionary<string, string> _icons = new();

        public static async Task InitAsync()
        {
            var assembly = typeof(CustomIconsProvider).GetTypeInfo().Assembly;
            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.EndsWith(".svg"))
                {
                    var resource = assembly.GetManifestResourceStream(resourceName);
                    if (resource is not null)
                    {
                        StreamReader reader = new(resource);
                        string text = await reader.ReadToEndAsync(); //hello world!
                        var name = resourceName.Replace("Sitko.Blockly.AntDesignComponents.Icons.", "")
                            .Replace(".svg", "");
                        _icons.Add(name, text);
                    }
                }
            }
        }

        public static RenderFragment GetIcon(string iconName) => builder =>
        {
            if (_icons.ContainsKey(iconName))
            {
                builder.AddMarkupContent(1, _icons[iconName]);
            }
        };
    }
}
