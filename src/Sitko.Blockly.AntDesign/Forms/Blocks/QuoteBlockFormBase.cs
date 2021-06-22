using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public abstract class QuoteBlockFormBase : BaseBlockForm<QuoteBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Text);
        }
    }
}
