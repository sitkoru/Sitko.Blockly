using Microsoft.AspNetCore.Components;
using Sitko.Core.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display;

public abstract class BlockComponent<TBlock> : BaseComponent
    where TBlock : ContentBlock
{
    [EditorRequired]
    [Parameter]
    public TBlock Block { get; set; } = null!;
}

public abstract class BlockComponent<TBlock, TListOptions> : BlockComponent<TBlock>
    where TListOptions : BlazorBlocklyListOptions
    where TBlock : ContentBlock
{
    [EditorRequired]
    [Parameter]
    public TListOptions Options { get; set; } = null!;
}
