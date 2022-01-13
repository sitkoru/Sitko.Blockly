using Sitko.Blockly.Display;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blazor.Display;

public class BlazorBlocklyListOptions : BlocklyListOptions
{
    public BlazorBlocklyListOptions(BlocksListMode mode = BlocksListMode.Full,
        IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
    {
    }
}
