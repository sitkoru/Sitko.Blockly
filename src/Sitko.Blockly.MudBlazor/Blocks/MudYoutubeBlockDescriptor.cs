using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record
    MudYoutubeBlockDescriptor : BlazorBlockDescriptor<YoutubeBlock, MudYoutubeBlockComponent, MudYoutubeBlockForm>
{
    public MudYoutubeBlockDescriptor(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => Icons.Custom.Brands.YouTube;
}
