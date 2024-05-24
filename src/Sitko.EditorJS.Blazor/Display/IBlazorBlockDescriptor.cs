using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public interface IBlazorBlockDescriptor : IBlockDescriptor
{
    string DisplayComponentCssClass => "";
    Type DisplayComponent { get; }
}

public interface IBlazorBlockDescriptor<TBlock> : IBlazorBlockDescriptor, IBlockDescriptor<TBlock>
    where TBlock : ContentBlock
{
}

// ReSharper disable UnusedTypeParameter
public interface IBlazorBlockDescriptor<TBlock, TDisplayComponent> : IBlazorBlockDescriptor<TBlock>
// ReSharper restore UnusedTypeParameter
    where TBlock : ContentBlock
    where TDisplayComponent : BlockComponent<TBlock>
{
}
