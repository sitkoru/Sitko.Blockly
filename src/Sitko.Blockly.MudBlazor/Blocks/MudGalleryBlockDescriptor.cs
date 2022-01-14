using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazor.Display.Blocks;
using Sitko.Blockly.MudBlazor.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazor.Blocks;

public record
    MudGalleryBlockDescriptor : BlazorBlockDescriptor<GalleryBlock, MudGalleryBlockComponent, MudGalleryBlockForm>
{
    public MudGalleryBlockDescriptor(ILocalizationProvider<GalleryBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon("gallery");
}
