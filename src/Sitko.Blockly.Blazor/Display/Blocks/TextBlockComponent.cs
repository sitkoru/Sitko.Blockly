using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    TextBlockComponent<TListOptions> : BlockComponent<TextBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
}
