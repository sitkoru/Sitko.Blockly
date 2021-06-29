using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntTwitterBlockDescriptor : TwitterBlockDescriptor, IBlazorBlockDescriptor<TwitterBlock>
    {
        public AntTwitterBlockDescriptor(IStringLocalizer<TwitterBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("twitter");
        public Type FormComponent => typeof(AntTwitterBlockForm<>);
        public Type DisplayComponent => typeof(AntTwitterBlockComponent<>);
    }
}
