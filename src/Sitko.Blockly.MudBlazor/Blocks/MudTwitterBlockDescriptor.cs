using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record
    MudTwitterBlockDescriptor : BlazorBlockDescriptor<TwitterBlock, MudTwitterBlockComponent, MudTwitterBlockForm>
{
    public MudTwitterBlockDescriptor(ILocalizationProvider<TwitterBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => Icons.Custom.Brands.Twitter;
}
