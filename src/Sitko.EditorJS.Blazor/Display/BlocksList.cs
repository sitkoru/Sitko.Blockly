using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Sitko.Core.Blazor.Components;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public class BlocksList : BaseComponent
{
    [EditorRequired]
    [Parameter]
    public IEnumerable<ContentBlock> EntityBlocks { get; set; } = null!;

    [PublicAPI]
    protected IBlazorBlockDescriptor[] BlockDescriptors { get; private set; } =
        Array.Empty<IBlazorBlockDescriptor>();

    [Inject] protected IEditorJS<IBlazorBlockDescriptor> Blockly { get; set; } = null!;

    protected ContentBlock[] Blocks => EntityBlocks.ToArray();

    protected override void Initialize()
    {
        base.Initialize();
        BlockDescriptors = Blockly.Descriptors.ToArray();
    }

    [PublicAPI]
    public static RenderFragment RenderBlock(IBlazorBlockDescriptor blockDescriptor, ContentBlock block) =>
        builder =>
        {
            var component = blockDescriptor.DisplayComponent;
            builder.OpenComponent(0, component);
            builder.AddAttribute(1, "Block", block);
            builder.CloseComponent();
        };
}
