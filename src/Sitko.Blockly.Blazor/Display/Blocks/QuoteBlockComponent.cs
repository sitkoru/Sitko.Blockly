using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class QuoteBlockComponent<TListOptions> : BlockComponent<QuoteBlock,
    TListOptions> where TListOptions : BlazorBlocklyListOptions
{
}
