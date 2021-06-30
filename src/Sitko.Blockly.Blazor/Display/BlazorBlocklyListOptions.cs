using Sitko.Core.Storage;

namespace Sitko.Blockly.Blazor.Display
{
    public class BlazorBlocklyListOptions
    {
        public IStorage? Storage { get; set; }
        public BlocksListMode Mode { get; set; } = BlocksListMode.Full;
    }
}
