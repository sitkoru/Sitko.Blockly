using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    public abstract class BaseBlockForm<TBlock> : ComponentBase
    {
        [Parameter] public TBlock Block { get; set; } = default!;

        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;
        protected FieldIdentifier FieldIdentifier { get; private set; }

        protected override void OnInitialized()
        {
            FieldIdentifier = CreateFieldIdentifier();
            base.OnInitialized();
        }

        protected abstract FieldIdentifier CreateFieldIdentifier();

        protected void NotifyChange()
        {
            CurrentEditContext.NotifyFieldChanged(FieldIdentifier);
            StateHasChanged();
        }
    }
}
