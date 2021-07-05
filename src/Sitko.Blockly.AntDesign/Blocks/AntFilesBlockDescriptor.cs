using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntFilesBlockDescriptor : FilesBlockDescriptor, IBlazorBlockDescriptor<FilesBlock>
    {
        public AntFilesBlockDescriptor(ILocalizationProvider<FilesBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public virtual RenderFragment Icon => builder => builder.AddIcon("attach");
        public virtual Type FormComponent => typeof(AntFilesBlockForm);
        public virtual Type DisplayComponent => typeof(AntFilesBlockComponent<>);
    }
}
