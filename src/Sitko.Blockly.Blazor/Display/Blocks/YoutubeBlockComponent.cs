namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        YoutubeBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.YoutubeBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
