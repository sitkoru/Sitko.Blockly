using AntDesign;
using Microsoft.AspNetCore.Components.Rendering;

namespace Sitko.Blockly.AntDesignComponents
{
    public static class RenderTreeBuilderExtensions
    {
        public static RenderTreeBuilder AddIcon(this RenderTreeBuilder builder, string icon)
        {
            builder.OpenComponent(1, typeof(Icon));
            builder.AddAttribute(1, nameof(Icon.Component), CustomIconsProvider.GetIcon(icon));
            builder.CloseComponent();
            return builder;
        }
    }
}
