using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazor.Display.Blocks;
using Sitko.Blockly.MudBlazor.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazor.Blocks;

public record MudCutBlockDescriptor : BlazorBlockDescriptor<CutBlock, MudCutBlockComponent, MudCutBlockForm>
{
    public MudCutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon(Icons.Filled.ContentCut);
}
