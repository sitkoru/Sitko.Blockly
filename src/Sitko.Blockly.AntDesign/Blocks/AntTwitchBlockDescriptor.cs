using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntTwitchBlockDescriptor : TwitchBlockDescriptor, IBlazorBlockDescriptor<TwitchBlock>
    {
        public AntTwitchBlockDescriptor(ILocalizationProvider<TwitchBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("twitch");
        public Type FormComponent => typeof(AntTwitchBlockForm<>);
        public Type DisplayComponent => typeof(AntTwitchBlockComponent<>);
    }
}
