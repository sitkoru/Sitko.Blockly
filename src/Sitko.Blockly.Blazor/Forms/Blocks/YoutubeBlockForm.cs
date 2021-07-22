using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class
        YoutubeBlockForm<TBlocklyFormOptions> : BlockForm<YoutubeBlock, TBlocklyFormOptions>
        where TBlocklyFormOptions : BlocklyFormOptions
    {
        protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.YoutubeId);
    }
}
