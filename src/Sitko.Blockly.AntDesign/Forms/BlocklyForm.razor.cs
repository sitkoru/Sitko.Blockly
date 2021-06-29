using System.Collections;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    public partial class BlocklyForm<TEntity, TForm> where TEntity : class, IBlocklyEntity
        where TForm : Sitko.Core.App.Blazor.Forms.BaseForm<TEntity>, IBlocklyForm
    {
        [Inject]
        protected IStringLocalizer<AntDesignBlocklyModule>? Localizer { get; set; }
    }
}
