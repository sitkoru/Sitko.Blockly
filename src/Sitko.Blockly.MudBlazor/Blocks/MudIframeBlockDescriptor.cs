using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record
    MudIframeBlockDescriptor : BlazorBlockDescriptor<IframeBlock, MudIframeBlockComponent, MudIFrameBlockForm>
{
    public MudIframeBlockDescriptor(ILocalizationProvider<IframeBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon(Icons.Filled.Code);
}
