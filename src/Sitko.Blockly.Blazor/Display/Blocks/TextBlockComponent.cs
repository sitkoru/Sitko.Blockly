namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        TextBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.TextBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
