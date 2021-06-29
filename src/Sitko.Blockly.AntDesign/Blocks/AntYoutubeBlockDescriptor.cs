using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntYoutubeBlockDescriptor : YoutubeBlockDescriptor, IBlazorBlockDescriptor<YoutubeBlock>
    {
        public AntYoutubeBlockDescriptor(IStringLocalizer<YoutubeBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("youtube");
        public Type FormComponent => typeof(AntYoutubeBlockForm<>);
        public Type DisplayComponent => typeof(AntYoutubeBlockComponent<>);
    }
}
