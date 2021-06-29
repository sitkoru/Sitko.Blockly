using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntCutBlockDescriptor : CutBlockDescriptor, IBlazorBlockDescriptor<CutBlock>
    {
        public AntCutBlockDescriptor(IStringLocalizer<CutBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("cut");
        public Type FormComponent => typeof(AntCutBlockForm<>);
        public Type DisplayComponent => typeof(AntCutBlockComponent<>);
    }
}
