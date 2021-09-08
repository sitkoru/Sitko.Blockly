using Sitko.Core.Storage;

namespace Sitko.Blockly.Display
{
    public class BlocklyListOptions
    {
        public IStorage? Storage { get; }
        public BlocksListMode Mode { get; }
        public string? EntityUrl { get; }

        public BlocklyListOptions(BlocksListMode mode = BlocksListMode.Full, IStorage? storage = null,
            string? entityUrl = null)
        {
            Storage = storage;
            Mode = mode;
            EntityUrl = entityUrl;
        }
    }
}
