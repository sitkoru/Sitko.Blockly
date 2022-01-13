using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display;

public abstract class BlockComponent<TBlock> : BaseComponent
    where TBlock : ContentBlock
{
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [Parameter]
    public TBlock Block { get; set; } = null!;
}

public abstract class BlockComponent<TBlock, TListOptions> : BlockComponent<TBlock>
    where TListOptions : BlazorBlocklyListOptions
    where TBlock : ContentBlock
{
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [Parameter]
    public TListOptions Options { get; set; } = null!;
}
