using Sitko.Core.App.Localization;
using Sitko.Core.Storage;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Image;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudImageBlockDescriptor : BlazorBlockDescriptor<ImageBlock<StorageItem>, MudImageBlockComponent>
{
    public MudImageBlockDescriptor(ILocalizationProvider<ImageBlock<StorageItem>> localizationProvider) : base(localizationProvider)
    {
    }
}
