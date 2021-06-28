using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class QuoteBlockForm<TForm> : BlockForm<TForm, QuoteBlock, IBlockFormStorageOptions,
        IBlockFormStorageOptions<TForm>> where TForm : BaseForm, IBlocklyForm
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Text);
        }
    }
}
