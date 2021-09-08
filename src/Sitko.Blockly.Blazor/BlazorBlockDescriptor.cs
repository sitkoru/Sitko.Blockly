namespace Sitko.Blockly.Blazor
{
    using System;
    using Core.App.Localization;
    using Display;
    using Forms;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Components;

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
        public Type FormComponent => typeof(TFormComponent);
        public Type DisplayComponent => typeof(TDisplayComponent);
    }
}
