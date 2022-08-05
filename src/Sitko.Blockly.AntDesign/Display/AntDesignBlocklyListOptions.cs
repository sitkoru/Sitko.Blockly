using Sitko.Blockly.Blazor.Display;
using Sitko.Blockly.Display;
using Sitko.Core.Storage;

namespace Sitko.Blockly.AntDesignComponents.Display;

public class AntDesignBlocklyListOptions : BlazorBlocklyListOptions
{
    public AntDesignBlocklyListOptions(BlocksListMode mode = BlocksListMode.Full,
        IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
    {
    }
}
