using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class QuoteBlockForm<TBlocklyFormOptions> : BlockForm<QuoteBlock, TBlocklyFormOptions>
        where TBlocklyFormOptions : BlocklyFormOptions
    {
        protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.Text);
    }
}
