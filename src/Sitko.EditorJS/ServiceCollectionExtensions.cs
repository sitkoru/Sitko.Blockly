using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Sitko.EditorJS.Configuration;

[assembly: InternalsVisibleTo("Sitko.EditorJS.Tests")]
namespace Sitko.EditorJS;

public static class ServiceCollectionExtensions
{
    public static IEditorJSBuilder AddEditorJS(this IServiceCollection serviceCollection)
    {
        var builder = new EditorJSBuilder(serviceCollection);
        return builder;
    }
}
