namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        FilesBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.FilesBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
