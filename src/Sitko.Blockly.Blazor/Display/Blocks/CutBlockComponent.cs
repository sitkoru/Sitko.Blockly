namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        CutBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.CutBlock, TListOptions>
        where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
