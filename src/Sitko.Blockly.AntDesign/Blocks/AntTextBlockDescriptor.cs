using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntTextBlockDescriptor : TextBlockDescriptor, IBlazorBlockDescriptor<TextBlock>
    {
        public AntTextBlockDescriptor(ILocalizationProvider<TextBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("text");
        public virtual Type FormComponent => typeof(AntTextBlockForm);
        public virtual Type DisplayComponent => typeof(AntTextBlockComponent<>);
    }
}
