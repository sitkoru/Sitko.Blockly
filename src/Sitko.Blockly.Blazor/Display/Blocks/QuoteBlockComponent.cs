namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class QuoteBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity,
        Sitko.Blockly.Blocks.QuoteBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
