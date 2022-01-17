using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record MudFilesBlockDescriptor : BlazorBlockDescriptor<FilesBlock, MudFilesBlockComponent, MudFilesBlockForm>
{
    public MudFilesBlockDescriptor(ILocalizationProvider<FilesBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon(Icons.Filled.Attachment);
}
