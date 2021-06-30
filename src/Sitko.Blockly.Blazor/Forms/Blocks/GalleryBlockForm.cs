using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class GalleryBlockForm<TForm> : BlockForm<TForm, GalleryBlock, IBlockFormStorageOptions,
        IBlockFormStorageOptions<TForm>> where TForm : BaseForm
    {
        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Pictures);
        }
    }
}
