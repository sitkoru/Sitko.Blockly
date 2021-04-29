using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesign.Forms.Blocks
{
    public abstract class TwitterBlockFormBase : BaseBlockForm<TwitterBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.TweetId);
        }
    }
}
