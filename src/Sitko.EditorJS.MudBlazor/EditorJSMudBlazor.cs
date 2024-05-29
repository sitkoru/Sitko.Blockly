using Microsoft.Extensions.DependencyInjection;
using Sitko.EditorJS.Blazor;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.MudBlazor.Blocks;

namespace Sitko.EditorJS.MudBlazor;

public class EditorJSMudBlazor<TImageData> : EditorJSBlazor<IBlazorBlockDescriptor>
    where TImageData: class, new()
{
    public EditorJSMudBlazor(IServiceCollection serviceCollection) : base(serviceCollection)
    {
        serviceCollection.AddEditorJS().AddDefaultBlocks<TImageData>();
        serviceCollection.AddSingleton<IBlazorBlockDescriptor, MudGalleryBlockDescriptor<TImageData>>();
        serviceCollection.AddSingleton<IBlazorBlockDescriptor, MudImageBlockDescriptor<TImageData>>();
        AddBlocks<EditorJSMudBlazor<TImageData>>();
    }
}
