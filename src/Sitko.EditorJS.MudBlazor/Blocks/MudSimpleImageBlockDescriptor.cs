using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.SimpleImage;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudSimpleImageBlockDescriptor : BlazorBlockDescriptor<SimpleImageBlock, MudSimpleImageBlockComponent>
{
    public MudSimpleImageBlockDescriptor(ILocalizationProvider<SimpleImageBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
