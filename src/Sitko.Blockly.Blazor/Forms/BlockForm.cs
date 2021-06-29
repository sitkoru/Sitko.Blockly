using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Core.App.Blazor.Components;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms
{
    public abstract class BlockForm : BaseComponent
    {
        [CascadingParameter] public EditContext CurrentEditContext { get; set; } = null!;
        public FieldIdentifier FieldIdentifier { get; private set; }

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

    public abstract class BlockForm<TForm, TBlock> : BlockForm
        where TForm : BaseForm, IBlocklyForm where TBlock : ContentBlock
    {
        [Parameter] public TBlock Block { get; set; } = default!;
        [Parameter] public TForm Form { get; set; } = default!;
        [Inject] protected IBlazorBlockDescriptor<TBlock> BlockDescriptor { get; set; } = default!;
    }

    public abstract class BlockForm<TForm, TBlock, TOptions, TFormOptions> : BlockForm<TForm, TBlock>
        where TForm : BaseForm, IBlocklyForm
        where TOptions : class, IBlockOptions
        where TFormOptions : class, TOptions, IBlockFormOptions<TForm>
        where TBlock : ContentBlock
    {
        protected TOptions Options { get; private set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
            TOptions? options = ScopedServices.GetService<TFormOptions>();
            if (options is null)
            {
                options = ScopedServices.GetService<TOptions>();
                if (options is null)
                {
                    throw new Exception("Block storage options is not configured");
                }
            }

            Options = options;
        }
    }
}
