namespace Sitko.Blockly.AntDesignComponents.Forms
{
    public partial class BlocklyForm<TEntity, TForm> where TEntity : class, IBlocklyEntity
        where TForm : Sitko.Core.App.Blazor.Forms.BaseForm<TEntity>, IBlocklyForm
    {
    }
}
