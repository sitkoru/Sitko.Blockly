using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class FilesBlockForm<TBlocklyFormOptions> : BlockForm<FilesBlock, TBlocklyFormOptions>
        where TBlocklyFormOptions : BlocklyFormOptions
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Files);
        }
    }
}
