﻿using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntYoutubeBlockDescriptor : YoutubeBlockDescriptor, IBlazorBlockDescriptor<YoutubeBlock>
    {
        public AntYoutubeBlockDescriptor(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
            localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("youtube");
        public virtual Type FormComponent => typeof(AntYoutubeBlockForm);
        public virtual Type DisplayComponent => typeof(AntYoutubeBlockComponent<>);
    }
}
