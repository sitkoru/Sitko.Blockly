using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks;

public record
    AntIframeBlockDescriptor : BlazorBlockDescriptor<IframeBlock, AntIframeBlockComponent, AntIFrameBlockForm>
{
    public AntIframeBlockDescriptor(ILocalizationProvider<IframeBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override RenderFragment Icon => builder => builder.AddIcon("embed");
}
