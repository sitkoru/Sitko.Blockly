using Microsoft.AspNetCore.Components.Rendering;
using MudBlazor;

namespace Sitko.Blockly.MudBlazor;

public static class RenderTreeBuilderExtensions
{
    public static RenderTreeBuilder AddIcon(this RenderTreeBuilder builder, string icon)
    {
        builder.OpenComponent(1, typeof(MudIcon));
        builder.AddAttribute(1, nameof(MudIcon.Icon), icon);
        builder.CloseComponent();
        return builder;
    }
}
