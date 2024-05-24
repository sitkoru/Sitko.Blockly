using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public abstract record BlazorBlockDescriptor<TBlock, TDisplayComponent> : BlockDescriptor<TBlock>,
    IBlazorBlockDescriptor<TBlock, TDisplayComponent>
    where TBlock : ContentBlock
    where TDisplayComponent : BlockComponent<TBlock>
{
    protected BlazorBlockDescriptor(ILocalizationProvider<TBlock> localizationProvider) : base(localizationProvider)
    {
    }

    public virtual Type DisplayComponent => typeof(TDisplayComponent);
}
