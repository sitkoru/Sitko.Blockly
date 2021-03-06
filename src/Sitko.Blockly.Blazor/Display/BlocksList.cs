using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Display
{
    using JetBrains.Annotations;

    public abstract class BlocksList<TOptions> : BaseComponent where TOptions : BlazorBlocklyListOptions
    {
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [Parameter] public IEnumerable<ContentBlock> EntityBlocks { get; set; } = null!;

        [PublicAPI]
        protected IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
            Array.Empty<IBlazorBlockDescriptor>();

        [Inject] protected IBlockly<IBlazorBlockDescriptor> Blockly { get; set; } = null!;

        protected ContentBlock[] Blocks => EntityBlocks.Where(b => b.Enabled).OrderBy(b => b.Position).ToArray();

        [Parameter] public TOptions Options { get; set; } = null!;

        protected override void Initialize()
        {
            base.Initialize();

            if (Options is null)
            {
                throw new InvalidOperationException("Provide options for BlocksList");
            }

            BlockDescriptors = Blockly.Descriptors.ToArray();
        }

        [PublicAPI]
        public RenderFragment RenderBlock(IBlazorBlockDescriptor blockDescriptor, ContentBlock block) =>
            builder =>
            {
                var component = blockDescriptor.DisplayComponent;
                builder.OpenComponent(0, component);
                builder.AddAttribute(1, "Block", block);
                builder.AddAttribute(2, "Options", Options);
                builder.CloseComponent();
            };
    }
}
