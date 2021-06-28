namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        GalleryBlockComponent<TEntity> : BlockComponent<TEntity, Sitko.Blockly.Blocks.GalleryBlock,
            IBlockStorageOptions> where TEntity : IBlocklyEntity
    {
    }
}
