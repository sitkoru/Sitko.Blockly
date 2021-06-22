using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public abstract class IFrameBlockFormBase : BaseBlockForm<IframeBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Src);
        }
    }
}
