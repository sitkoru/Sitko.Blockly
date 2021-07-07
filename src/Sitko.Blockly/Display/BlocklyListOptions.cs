using Sitko.Core.Storage;

namespace Sitko.Blockly.Display
{
    public class BlocklyListOptions
    {
        public IStorage? Storage { get; set; }
        public BlocksListMode Mode { get; set; } = BlocksListMode.Full;
    }
}
