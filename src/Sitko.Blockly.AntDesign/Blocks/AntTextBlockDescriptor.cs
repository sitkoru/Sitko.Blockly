using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntTextBlockDescriptor : TextBlockDescriptor, IBlazorBlockDescriptor<TextBlock>
    {
        public AntTextBlockDescriptor(IStringLocalizer<TextBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("text");
        public Type FormComponent => typeof(AntTextBlockForm<>);
        public Type DisplayComponent => typeof(AntTextBlockComponent<>);
    }
}
