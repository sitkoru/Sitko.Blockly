using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace Sitko.Blockly.AntDesignComponents;

internal static class CustomIconsProvider
{
    private static readonly Dictionary<string, string> Icons = new();

    public static async Task InitAsync(Dictionary<Assembly, string> sources)
    {
        foreach (var (assembly, prefix) in sources)
        {
            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                if (resourceName.EndsWith(".svg", StringComparison.InvariantCulture))
                {
                    var resource = assembly.GetManifestResourceStream(resourceName);
                    if (resource is not null)
                    {
                        StreamReader reader = new(resource);
                        var text = await reader.ReadToEndAsync();
                        var name = resourceName.Replace(prefix, "")
                            .Replace(".svg", "");
                        Icons.Add(name, text);
                    }
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

    public static bool HasIcon(string icon) => Icons.ContainsKey(icon);
}
