using Sitko.Core.App.Localization;
using Sitko.Core.Storage;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Gallery;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudGalleryBlockDescriptor : BlazorBlockDescriptor<GalleryBlock<StorageItem>, MudGalleryBlockComponent>
{
    public MudGalleryBlockDescriptor(ILocalizationProvider<GalleryBlock<StorageItem>> localizationProvider) : base(localizationProvider)
    {
    }
}
