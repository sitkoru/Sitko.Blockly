using Sitko.Blockly.Blazor.Display;
using Sitko.Blockly.Display;
using Sitko.Core.Storage;

namespace Sitko.Blockly.MudBlazorComponents.Display;

public class MudBlazorBlocklyListOptions : BlazorBlocklyListOptions
{
    public MudBlazorBlocklyListOptions(BlocksListMode mode = BlocksListMode.Full,
        IStorage? storage = null, string? entityUrl = null) : base(mode, storage, entityUrl)
    {
    }
}
