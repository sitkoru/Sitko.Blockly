using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesign.Forms.Blocks
{
    public abstract class PictureBlockFormBase : BaseBlockForm<PictureBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Picture);
        }
    }
}
