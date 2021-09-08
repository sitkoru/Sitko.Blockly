using Sitko.Blockly.Blazor.Display;

namespace Sitko.Blockly.AntDesignComponents.Display
{
    using Core.Storage;
    using Sitko.Blockly.Display;

    public class AntDesignBlocklyListOptions : BlazorBlocklyListOptions
    {
        public AntDesignBlocklyListOptions(BlocksListMode mode = BlocksListMode.Full,
            IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
        {
        }
    }
}
