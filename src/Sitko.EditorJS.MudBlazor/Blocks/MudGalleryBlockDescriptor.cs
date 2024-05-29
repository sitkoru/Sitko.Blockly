using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Gallery;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudGalleryBlockDescriptor<TImageData> : BlazorBlockDescriptor<GalleryBlock<TImageData>, MudGalleryBlockComponent<TImageData>>
    where TImageData: class, new()
{
    public MudGalleryBlockDescriptor(ILocalizationProvider<GalleryBlock<TImageData>> localizationProvider) : base(localizationProvider)
    {
    }
}
