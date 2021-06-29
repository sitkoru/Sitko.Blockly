using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display
{
    public abstract class BlocksList<TEntity> : BaseComponent where TEntity : IBlocklyEntity
    {
        [Parameter] public TEntity Entity { get; set; } = default!;
        [Parameter] public string EntityUrl { get; set; } = null!;
        [Parameter] public BlocksListMode Mode { get; set; } = BlocksListMode.Full;

        protected IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;

        protected ContentBlock[] Blocks => Entity.Blocks.Where(b => b.Enabled).OrderBy(b => b.Position).ToArray();

        protected override void OnInitialized()
        {
            base.OnInitialized();

            BlockDescriptors = Blockly.Descriptors.ToArray();
            Context = new BlockListContext<TEntity>(Entity, EntityUrl, Mode);
        }

        protected BlockListContext<TEntity> Context { get; private set; } = null!;

        public RenderFragment RenderBlock(IBlazorBlockDescriptor blockDescriptor, ContentBlock block)
        {
            return builder =>
            {
                var component = blockDescriptor.DisplayComponent;
                if (blockDescriptor.FormComponent.IsGenericTypeDefinition)
                {
                    component = blockDescriptor.DisplayComponent.MakeGenericType(typeof(TEntity));
                }

                builder.OpenComponent(0, component);
                builder.AddAttribute(1, "Block", block);
                builder.AddAttribute(2, "Context", Context);
                builder.CloseComponent();
            };
        }
    }

    public enum BlocksListMode
    {
        Preview,
        Full
    }

    public record BlockListContext<TEntity>(TEntity Entity, string EntityUrl, BlocksListMode Mode)
        where TEntity : IBlocklyEntity;
}
