using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record MudCutBlockDescriptor : BlazorBlockDescriptor<CutBlock, MudCutBlockComponent, MudCutBlockForm>
{
    public MudCutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
    {
    }

    public override string Icon => Icons.Filled.ContentCut;
}
