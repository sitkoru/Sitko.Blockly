using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor.Display;
using Sitko.Blockly.Blazor.Forms;

namespace Sitko.Blockly.Blazor;

public interface IBlazorBlockDescriptor : IBlockDescriptor
{
    string DisplayComponentCssClass => "";
    string FormComponentCssClass => "";
    Type FormComponent { get; }
    Type DisplayComponent { get; }
}

public interface IBlazorBlockDescriptor<TBlock> : IBlazorBlockDescriptor, IBlockDescriptor<TBlock>
    where TBlock : ContentBlock
{
}

// ReSharper disable UnusedTypeParameter
public interface IBlazorBlockDescriptor<TBlock, TDisplayComponent, TFormComponent> : IBlazorBlockDescriptor<TBlock>
    // ReSharper restore UnusedTypeParameter
    where TBlock : ContentBlock
    where TDisplayComponent : BlockComponent<TBlock>
    where TFormComponent : BlockForm<TBlock>
{
}
