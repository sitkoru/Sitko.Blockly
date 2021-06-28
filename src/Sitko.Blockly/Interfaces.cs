using Sitko.Core.Storage;

namespace Sitko.Blockly
{
    public interface IBlockOptions
    {
    }

    public interface IBlockStorageOptions : IBlockOptions
    {
        IStorage Storage { get; }
    }

    public abstract class BlockStorageOptions : IBlockStorageOptions
    {
        protected BlockStorageOptions(IStorage storage)
        {
            Storage = storage;
        }

        public IStorage Storage { get; }
    }
}
