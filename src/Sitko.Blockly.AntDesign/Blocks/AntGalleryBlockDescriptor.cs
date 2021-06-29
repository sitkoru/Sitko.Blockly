using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntGalleryBlockDescriptor : GalleryBlockDescriptor, IBlazorBlockDescriptor<GalleryBlock>
    {
        public AntGalleryBlockDescriptor(IStringLocalizer<GalleryBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("gallery");
        public Type FormComponent => typeof(AntGalleryBlockForm<>);
        public Type DisplayComponent => typeof(AntGalleryBlockComponent<>);
    }
}
