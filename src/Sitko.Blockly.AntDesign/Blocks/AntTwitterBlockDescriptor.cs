using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntTwitterBlockDescriptor : TwitterBlockDescriptor, IBlazorBlockDescriptor<TwitterBlock>
    {
        public AntTwitterBlockDescriptor(ILocalizationProvider<TwitterBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("twitter");
        public Type FormComponent => typeof(AntTwitterBlockForm);
        public Type DisplayComponent => typeof(AntTwitterBlockComponent<>);
    }
}
