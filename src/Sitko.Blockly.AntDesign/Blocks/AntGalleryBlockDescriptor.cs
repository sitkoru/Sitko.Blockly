using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntGalleryBlockDescriptor : GalleryBlockDescriptor, IBlazorBlockDescriptor<GalleryBlock>
    {
        public AntGalleryBlockDescriptor(ILocalizationProvider<GalleryBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("gallery");
        public virtual Type FormComponent => typeof(AntGalleryBlockForm);
        public virtual Type DisplayComponent => typeof(AntGalleryBlockComponent<>);
    }
}
