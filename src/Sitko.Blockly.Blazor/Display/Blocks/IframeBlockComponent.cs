using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    IframeBlockComponent<TListOptions> : BlockComponent<IframeBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
}
