using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public class TextBlockFormBase : BaseBlockForm<TextBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Text);
        }
    }
}
