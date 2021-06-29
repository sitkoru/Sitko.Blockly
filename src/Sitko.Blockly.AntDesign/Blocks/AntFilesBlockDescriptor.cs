using System;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntFilesBlockDescriptor : FilesBlockDescriptor, IBlazorBlockDescriptor<FilesBlock>
    {
        public AntFilesBlockDescriptor(IStringLocalizer<FilesBlock>? localizer = null) : base(localizer)
        {
        }

        public RenderFragment Icon => builder => builder.AddIcon("attach");
        public Type FormComponent => typeof(AntFilesBlockForm<>);
        public Type DisplayComponent => typeof(AntFilesBlockComponent<>);
    }
}
