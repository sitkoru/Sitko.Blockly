using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public abstract record BlazorBlockDescriptor<TBlock, TDisplayComponent> : BlockDescriptor<TBlock>,
    IBlazorBlockDescriptor<TBlock, TDisplayComponent>
    where TBlock : ContentBlock
    where TDisplayComponent : BlockComponent<TBlock>
{
    public virtual Type DisplayComponent => typeof(TDisplayComponent);
}
