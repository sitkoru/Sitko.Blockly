namespace Sitko.Blockly.Display
{
    public enum BlocksListMode
    {
        Preview,
        Full
    }

    public record BlockListContext(string EntityUrl, BlocksListMode Mode);
}
