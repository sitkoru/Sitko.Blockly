using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks;

public record AntFilesBlockDescriptor : BlazorBlockDescriptor<FilesBlock, AntFilesBlockComponent, AntFilesBlockForm>
{
    public AntFilesBlockDescriptor(ILocalizationProvider<FilesBlock> localizationProvider) : base(
        localizationProvider)
    {
    }

    public override string Icon => "attach";
}
