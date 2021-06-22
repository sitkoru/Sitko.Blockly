using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public abstract class BaseCutBlockForm : BaseBlockForm<CutBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.ButtonText);
        }
    }
}
