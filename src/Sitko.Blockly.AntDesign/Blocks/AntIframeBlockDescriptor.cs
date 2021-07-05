using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntIframeBlockDescriptor : IframeBlockDescriptor, IBlazorBlockDescriptor<IframeBlock>
    {
        public AntIframeBlockDescriptor(ILocalizationProvider<IframeBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("embed");
        public virtual Type FormComponent => typeof(AntIFrameBlockForm);
        public virtual Type DisplayComponent => typeof(AntIframeBlockComponent<>);
    }
}
