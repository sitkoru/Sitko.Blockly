﻿using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazor.Display.Blocks;
using Sitko.Blockly.MudBlazor.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazor.Blocks;

public record
    MudTwitterBlockDescriptor : BlazorBlockDescriptor<TwitterBlock, MudTwitterBlockComponent, MudTwitterBlockForm>
{
    public MudTwitterBlockDescriptor(ILocalizationProvider<TwitterBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon("twitter");
}
