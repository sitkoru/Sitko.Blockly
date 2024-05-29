using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Image;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudImageBlockDescriptor<TImageData> : BlazorBlockDescriptor<ImageBlock<TImageData>, MudImageBlockComponent<TImageData>>
    where TImageData: class, new()
{
    public MudImageBlockDescriptor(ILocalizationProvider<ImageBlock<TImageData>> localizationProvider) : base(localizationProvider)
    {
    }
}
