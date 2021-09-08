using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.Display;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.AntDesignComponents.Blocks
{
    public record AntCutBlockDescriptor : BlazorBlockDescriptor<CutBlock, AntCutBlockComponent, AntCutBlockForm>
    {
        public AntCutBlockDescriptor(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider)
        {
        }

        public override RenderFragment Icon => builder => builder.AddIcon("cut");

        public bool ShouldRender(BlockListContext context, ContentBlock block) =>
            context.Mode == BlocksListMode.Preview;

        public bool ShouldRenderNext(BlockListContext context, ContentBlock block) =>
            context.Mode == BlocksListMode.Full;
    }
}
