using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor.Display;
using Sitko.Blockly.Blazor.Forms;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blazor;

[PublicAPI]
public abstract record BlazorBlockDescriptor<TBlock, TDisplayComponent, TFormComponent> : BlockDescriptor<TBlock>,
    IBlazorBlockDescriptor<TBlock, TDisplayComponent, TFormComponent>
    where TBlock : ContentBlock
    where TDisplayComponent : BlockComponent<TBlock>
    where TFormComponent : BlockForm<TBlock>
{
    protected BlazorBlockDescriptor(ILocalizationProvider<TBlock> localizationProvider) : base(localizationProvider)
    {
    }

    public abstract RenderFragment Icon { get; }
    public virtual Type FormComponent => typeof(TFormComponent);
    public virtual Type DisplayComponent => typeof(TDisplayComponent);
}
