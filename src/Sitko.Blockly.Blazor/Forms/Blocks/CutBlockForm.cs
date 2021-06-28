using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class CutBlockForm<TForm> : BlockForm<TForm, CutBlock> where TForm : BaseForm, IBlocklyForm
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.ButtonText);
        }
    }
}
