namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        GalleryBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.GalleryBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
