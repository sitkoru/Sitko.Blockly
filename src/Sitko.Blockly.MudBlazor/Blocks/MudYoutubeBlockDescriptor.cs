﻿using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazor.Display.Blocks;
using Sitko.Blockly.MudBlazor.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazor.Blocks;

public record
    MudYoutubeBlockDescriptor : BlazorBlockDescriptor<YoutubeBlock, MudYoutubeBlockComponent, MudYoutubeBlockForm>
{
    public MudYoutubeBlockDescriptor(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon("youtube");
}
