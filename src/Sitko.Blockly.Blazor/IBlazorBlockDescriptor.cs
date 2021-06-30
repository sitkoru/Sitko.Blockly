using System;
using Microsoft.AspNetCore.Components;

namespace Sitko.Blockly.Blazor
{
    public interface IBlazorBlockDescriptor : IBlockDescriptor
    {
        RenderFragment Icon { get; }
        Type FormComponent { get; }
        Type DisplayComponent { get; }
    }
}