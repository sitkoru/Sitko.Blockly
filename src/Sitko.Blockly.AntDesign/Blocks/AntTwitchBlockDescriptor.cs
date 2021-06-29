using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntTwitchBlockDescriptor : TwitchBlockDescriptor, IBlazorBlockDescriptor<TwitchBlock>
    {
        public AntTwitchBlockDescriptor(IStringLocalizer<TwitchBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("twitch");
        public Type FormComponent => typeof(AntTwitchBlockForm<>);
        public Type DisplayComponent => typeof(AntTwitchBlockComponent<>);
    }
}
