namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        GalleryBlockComponent<TListOptions> : BlockComponent<Sitko.Blockly.Blocks.GalleryBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
