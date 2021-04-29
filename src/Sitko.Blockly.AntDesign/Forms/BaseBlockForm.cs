using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Sitko.Blockly.AntDesign.Forms
{
    public abstract class BaseBlockForm<TBlock> : ComponentBase
    {
        [Parameter] public TBlock Block { get; set; }

        [CascadingParameter] public EditContext CurrentEditContext { get; set; }
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
