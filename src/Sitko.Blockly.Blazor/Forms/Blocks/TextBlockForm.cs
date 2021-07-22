using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class TextBlockForm<TBlocklyFormOptions> : BlockForm<TextBlock, TBlocklyFormOptions>
        where TBlocklyFormOptions : BlocklyFormOptions
    {
        protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.Text);
    }
}
