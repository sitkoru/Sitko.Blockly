using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    FilesBlockComponent<TListOptions> : BlockComponent<FilesBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
}
