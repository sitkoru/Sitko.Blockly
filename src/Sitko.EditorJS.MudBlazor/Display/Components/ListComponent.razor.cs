using Microsoft.AspNetCore.Components;
using Sitko.EditorJS.Blocks.List;

namespace Sitko.EditorJS.MudBlazor.Display.Components;

public partial class ListComponent
{
    [Parameter] public ListBlockItemData[] Items { get; set; } = [];
    [Parameter] public string Style { get; set; } = "";
    [Parameter] public int Level { get; set; }
}
