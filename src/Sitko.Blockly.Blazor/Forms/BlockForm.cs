using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Sitko.Core.Blazor.Components;

namespace Sitko.Blockly.Blazor.Forms;

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
    [EditorRequired]
    [Parameter]
    public TBlock Block { get; set; } = default!;

    [Inject] protected IBlazorBlockDescriptor<TBlock> BlockDescriptor { get; set; } = default!;
}

public abstract class BlockForm<TBlock, TBlocklyFormOptions> : BlockForm<TBlock>
    where TBlock : ContentBlock
    where TBlocklyFormOptions : BlocklyFormOptions
{
    [EditorRequired]
    [Parameter]
    public TBlocklyFormOptions FormOptions { get; set; } = null!;
}
