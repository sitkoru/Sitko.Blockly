﻿using Microsoft.AspNetCore.Components;
using MudBlazor;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.MudBlazorComponents.Display.Blocks;
using Sitko.Blockly.MudBlazorComponents.Forms.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Blocks;

public record
    MudTwitchBlockDescriptor : BlazorBlockDescriptor<TwitchBlock, MudTwitchBlockComponent, MudTwitchBlockForm>
{
    public MudTwitchBlockDescriptor(ILocalizationProvider<TwitchBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => Icons.Filled.Stream; // TODO: Get twitch icon
}
