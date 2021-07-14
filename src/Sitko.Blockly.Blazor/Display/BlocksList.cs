using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Display;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display
{
    using JetBrains.Annotations;

    public abstract class BlocksList<TEntity, TOptions> : BaseComponent where TOptions : BlazorBlocklyListOptions, new()
    {
        [Parameter] public TEntity Entity { get; set; } = default!;
        [Parameter] public string EntityUrl { get; set; } = null!;
        [Parameter] public IEnumerable<ContentBlock> EntityBlocks { get; set; } = null!;

        [PublicAPI]
        protected IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;

        protected ContentBlock[] Blocks => EntityBlocks.Where(b => b.Enabled).OrderBy(b => b.Position).ToArray();

        protected TOptions ListOptions { get; set; } = new();

        [Parameter]
        public TOptions? Options
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

        protected override void Initialize()
        {
            base.Initialize();
            BlockDescriptors = Blockly.Descriptors.ToArray();
            Context = new BlockListContext<TEntity>(Entity, EntityUrl, ListOptions.Mode);
        }

        protected BlockListContext<TEntity> Context { get; private set; } = null!;

        [PublicAPI]
        public RenderFragment RenderBlock(IBlazorBlockDescriptor blockDescriptor, ContentBlock block) =>
            builder =>
            {
                var component = blockDescriptor.DisplayComponent;
                if (blockDescriptor.DisplayComponent.IsGenericTypeDefinition)
                {
                    component = blockDescriptor.DisplayComponent.MakeGenericType(typeof(TEntity));
                }

                builder.OpenComponent(0, component);
                builder.AddAttribute(1, "Block", block);
                builder.AddAttribute(2, "Context", Context);
                builder.AddAttribute(3, "Options", ListOptions);
                builder.CloseComponent();
            };
    }
}
