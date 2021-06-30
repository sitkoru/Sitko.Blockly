using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display
{
    public abstract class BlockComponent<TEntity, TBlock, TListOptions> : BaseComponent
        where TListOptions : BlazorBlocklyListOptions, new()
        where TBlock : ContentBlock
    {
        [Parameter] public TBlock Block { get; set; } = null!;
        [Parameter] public BlockListContext<TEntity> Context { get; set; } = null!;

        protected TListOptions ListOptions = new();

        [Parameter]
        public TListOptions? Options
        {
            get
            {
                return ListOptions;
            }
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
