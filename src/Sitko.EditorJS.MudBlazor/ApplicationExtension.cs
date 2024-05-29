using Microsoft.Extensions.DependencyInjection;

namespace Sitko.EditorJS.MudBlazor;

public static class ApplicationExtension
{
    public static EditorJSMudBlazor<TImageData> AddEditorJSMudBlazor<TImageData>(this IServiceCollection serviceCollection)
        where TImageData : class, new()
    {
        var builder = new EditorJSMudBlazor<TImageData>(serviceCollection);
        return builder;
    }
}
