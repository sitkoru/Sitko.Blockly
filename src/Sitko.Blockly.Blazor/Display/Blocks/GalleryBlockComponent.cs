using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    GalleryBlockComponent<TListOptions> : BlockComponent<GalleryBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
}
