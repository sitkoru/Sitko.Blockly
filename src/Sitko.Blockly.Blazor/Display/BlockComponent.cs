using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Display;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display
{
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
        where TListOptions : BlazorBlocklyListOptions, new()
        where TBlock : ContentBlock
    {
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [Parameter]
        public BlockListContext Context { get; set; } = null!;

        protected TListOptions ListOptions { get; set; } = new();

        [Parameter]
        public TListOptions? Options
        {
            get => ListOptions;
            set
            {
                if (value is not null)
                {
                    ListOptions = value;
                }
            }
        }
    }
}
