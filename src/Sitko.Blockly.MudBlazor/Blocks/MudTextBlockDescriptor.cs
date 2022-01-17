using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record MudTextBlockDescriptor : BlazorBlockDescriptor<TextBlock, MudTextBlockComponent, MudTextBlockForm>
{
    public MudTextBlockDescriptor(ILocalizationProvider<TextBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon("text");
}
