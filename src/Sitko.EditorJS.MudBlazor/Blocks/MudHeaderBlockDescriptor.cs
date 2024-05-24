using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Header;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudHeaderBlockDescriptor : BlazorBlockDescriptor<HeaderBlock, MudHeaderBlockComponent>
{
    public MudHeaderBlockDescriptor(ILocalizationProvider<HeaderBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
