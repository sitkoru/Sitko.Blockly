namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class QuoteBlockComponent<TEntity> : BlockComponent<TEntity, Sitko.Blockly.Blocks.QuoteBlock,
        IBlockStorageOptions> where TEntity : IBlocklyEntity
    {
    }
}
