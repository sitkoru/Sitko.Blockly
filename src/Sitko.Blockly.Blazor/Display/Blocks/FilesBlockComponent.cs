namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        FilesBlockComponent<TEntity> : BlockComponent<TEntity, Sitko.Blockly.Blocks.FilesBlock, IBlockStorageOptions>
        where TEntity : IBlocklyEntity
    {
    }
}
