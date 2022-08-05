using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    YoutubeBlockComponent<TListOptions> : BlockComponent<YoutubeBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
}
