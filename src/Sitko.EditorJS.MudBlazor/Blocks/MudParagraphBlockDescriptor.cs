using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks.Paragraph;
using Sitko.EditorJS.MudBlazor.Display.Blocks;

namespace Sitko.EditorJS.MudBlazor.Blocks;

public record MudParagraphBlockDescriptor : BlazorBlockDescriptor<ParagraphBlock, MudParagraphBlockComponent>
{
    public MudParagraphBlockDescriptor(ILocalizationProvider<ParagraphBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
