namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        FilesBlockComponent<TListOptions> : BlockComponent<Sitko.Blockly.Blocks.FilesBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
    }
}
