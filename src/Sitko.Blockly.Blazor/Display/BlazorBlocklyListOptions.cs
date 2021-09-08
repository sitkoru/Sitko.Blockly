using Sitko.Blockly.Display;

namespace Sitko.Blockly.Blazor.Display
{
    using Core.Storage;

    public class BlazorBlocklyListOptions : BlocklyListOptions
    {
        public BlazorBlocklyListOptions(BlocksListMode mode = BlocksListMode.Full,
            IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
        {
        }
    }
}
