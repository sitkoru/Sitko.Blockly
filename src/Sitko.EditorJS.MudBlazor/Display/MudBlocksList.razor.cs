using Microsoft.AspNetCore.Components;
using Sitko.EditorJS.Blocks;
using Sitko.EditorJS.Data;

namespace Sitko.EditorJS.MudBlazor.Display;

public partial class MudBlocksList
{
    [Parameter] public EditorJSData Data { get; set; }

    [Inject] private IBlocksAccessor BlocksAccessor { get; set; } = null!;

    private void BlockToType(ContentBlock block)
    {
        var b = BlocksAccessor.GetBlockTypes()[block.Id];
    }
}
