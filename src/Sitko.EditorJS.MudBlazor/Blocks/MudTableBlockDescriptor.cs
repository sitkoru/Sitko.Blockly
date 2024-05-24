using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Table;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudTableBlockDescriptor : BlazorBlockDescriptor<TableBlock, MudTableBlockComponent>
{
    public MudTableBlockDescriptor(ILocalizationProvider<TableBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
