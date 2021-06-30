using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display
{
    public abstract class BlockComponent<TEntity, TBlock, TOptions> : BlockComponent<TEntity, TBlock>
        where TOptions : class, IBlockOptions
        where TBlock : ContentBlock
    {
        protected TOptions Options { get; private set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            TOptions? options = ScopedServices.GetService<TOptions>();
            if (options is null)
            {
                options = ScopedServices.GetService<TOptions>();
                if (options is null)
                {
                    throw new Exception("Block storage options is not configured");
                }
            }

            Options = options;
        }
    }

    public abstract class BlockComponent<TEntity, TBlock> : BaseComponent
        where TBlock : ContentBlock
    {
        [Parameter] public TBlock Block { get; set; } = null!;
        [Parameter] public BlockListContext<TEntity> Context { get; set; } = null!;
    }
}
