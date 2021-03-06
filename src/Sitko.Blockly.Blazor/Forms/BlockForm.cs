using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sitko.Core.App.Blazor.Components;

namespace Sitko.Blockly.Blazor.Forms
{
    using System.Threading.Tasks;

    public abstract class BlockForm : BaseComponent
    {
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;
        public FieldIdentifier FieldIdentifier { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            FieldIdentifier = CreateFieldIdentifier();
        }

        protected abstract FieldIdentifier CreateFieldIdentifier();

        protected void NotifyChange()
        {
            CurrentEditContext.NotifyFieldChanged(FieldIdentifier);
            StateHasChanged();
        }

        public virtual Task OnDeleteAsync() => Task.CompletedTask;
    }

    public abstract class BlockForm<TBlock> : BlockForm where TBlock : ContentBlock
    {
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [Parameter]
        public TBlock Block { get; set; } = default!;

        [Inject] protected IBlazorBlockDescriptor<TBlock> BlockDescriptor { get; set; } = default!;
    }

    public abstract class BlockForm<TBlock, TBlocklyFormOptions> : BlockForm<TBlock>
        where TBlock : ContentBlock
        where TBlocklyFormOptions : BlocklyFormOptions
    {
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        [Parameter]
        public TBlocklyFormOptions FormOptions { get; set; } = null!;
    }
}
