using System;
using Microsoft.AspNetCore.Components;

namespace Sitko.Blockly.Blazor
{
    using Display;
    using Forms;

    public interface IBlazorBlockDescriptor : IBlockDescriptor
    {
        string DisplayComponentCssClass => "";
        string FormComponentCssClass => "";
        RenderFragment Icon { get; }
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
}
