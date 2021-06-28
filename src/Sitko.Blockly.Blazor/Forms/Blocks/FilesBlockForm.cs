using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class FilesBlockForm<TForm> : BlockForm<TForm, FilesBlock, IBlockFormStorageOptions,
        IBlockFormStorageOptions<TForm>> where TForm : BaseForm, IBlocklyForm
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Files);
        }
    }
}
