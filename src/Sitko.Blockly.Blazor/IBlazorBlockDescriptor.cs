using System;
using Microsoft.AspNetCore.Components;

namespace Sitko.Blockly.Blazor
{
    public interface IBlazorBlockDescriptor : IBlockDescriptor
    {
        RenderFragment Icon { get; }
        Type FormComponent { get; }
        string FormComponentCssClass => "";
        Type DisplayComponent { get; }
        string DisplayComponentCssClass => "";
    }
}
