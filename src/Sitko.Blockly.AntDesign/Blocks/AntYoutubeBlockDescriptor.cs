using System;
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

        public RenderFragment Icon => builder => builder.AddIcon("youtube");
        public Type FormComponent => typeof(AntYoutubeBlockForm<>);
        public Type DisplayComponent => typeof(AntYoutubeBlockComponent<>);
    }
}
