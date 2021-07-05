using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntCutBlockDescriptor : CutBlockDescriptor, IBlazorBlockDescriptor<CutBlock>
    {
        public AntCutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("cut");
        public virtual Type FormComponent => typeof(AntCutBlockForm);
        public virtual Type DisplayComponent => typeof(AntCutBlockComponent<>);
    }
}
