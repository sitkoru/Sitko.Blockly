namespace Sitko.Blockly.Display
{
    public enum BlocksListMode
    {
        Preview,
        Full
    }

    public record BlockListContext(BlocksListMode Mode);

    public record BlockListContext<TEntity>
        (TEntity Entity, string EntityUrl, BlocksListMode Mode) : BlockListContext(Mode);
}
