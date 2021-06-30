namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        IframeBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.IframeBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
