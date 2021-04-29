using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesign.Forms.Blocks
{
    public abstract class TwitchBlockFormBase : BaseBlockForm<TwitchBlock>
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.ChannelId);
        }
    }
}
