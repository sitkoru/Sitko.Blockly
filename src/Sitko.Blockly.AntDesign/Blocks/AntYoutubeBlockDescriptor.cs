﻿using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks;

public record
    AntYoutubeBlockDescriptor : BlazorBlockDescriptor<YoutubeBlock, AntYoutubeBlockComponent, AntYoutubeBlockForm>
{
    public AntYoutubeBlockDescriptor(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => "youtube";
}
