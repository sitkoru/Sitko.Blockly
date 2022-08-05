using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks;

public record AntTextBlockDescriptor : BlazorBlockDescriptor<TextBlock, AntTextBlockComponent, AntTextBlockForm>
{
    public AntTextBlockDescriptor(ILocalizationProvider<TextBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => "text";
}
