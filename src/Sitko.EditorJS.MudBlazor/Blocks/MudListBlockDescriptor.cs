using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.List;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudListBlockDescriptor : BlazorBlockDescriptor<ListBlock, MudListBlockComponent>
{
    public MudListBlockDescriptor(ILocalizationProvider<ListBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
