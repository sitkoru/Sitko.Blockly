using Microsoft.AspNetCore.Components;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public abstract class BlockComponent<TBlock> : ComponentBase
    where TBlock : ContentBlock
{
    [EditorRequired]
    [Parameter]
    public TBlock Block { get; set; } = null!;
}
