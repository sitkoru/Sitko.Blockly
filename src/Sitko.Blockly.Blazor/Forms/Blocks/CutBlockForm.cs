using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class CutBlockForm<TBlocklyFormOptions> : BlockForm<CutBlock, TBlocklyFormOptions>
        where TBlocklyFormOptions : BlocklyFormOptions
    {
        protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.ButtonText);
    }
}
