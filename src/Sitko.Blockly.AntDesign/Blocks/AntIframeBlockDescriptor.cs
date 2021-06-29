using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntIframeBlockDescriptor : IframeBlockDescriptor, IBlazorBlockDescriptor<IframeBlock>
    {
        public AntIframeBlockDescriptor(IStringLocalizer<IframeBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("embed");
        public Type FormComponent => typeof(AntIFrameBlockForm<>);
        public Type DisplayComponent => typeof(AntIframeBlockComponent<>);
    }
}
