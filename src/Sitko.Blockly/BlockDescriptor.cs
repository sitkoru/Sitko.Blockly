using System;
using Sitko.Blockly.Display;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly
{
    public interface IBlockDescriptor
    {
        string Title { get; }
        Type Type { get; }
        int MaxCount { get; }
        int Priority { get; }
        string Key { get; }
        bool ShouldRender(BlockListContext context, ContentBlock block) => true;
        bool ShouldRenderNext(BlockListContext context, ContentBlock block) => true;
    }

    // ReSharper disable once UnusedTypeParameter
    public interface IBlockDescriptor<TBlock> : IBlockDescriptor where TBlock : ContentBlock
    {
    }

    public abstract record BlockDescriptor : IBlockDescriptor
    {
        public abstract string Title { get; }
        public abstract Type Type { get; }
        public abstract string Key { get; }
        public virtual int MaxCount { get; } = 0;
        public virtual int Priority { get; } = int.MaxValue;
    }

    public abstract record BlockDescriptor<TBlock> : BlockDescriptor, IBlockDescriptor<TBlock>
        where TBlock : ContentBlock
    {
        protected ILocalizationProvider<TBlock> LocalizationProvider { get; }

        public BlockDescriptor(ILocalizationProvider<TBlock> localizationProvider)
        {
            LocalizationProvider = localizationProvider;
        }

        public override Type Type => typeof(TBlock);

        public override string Title => LocalizationProvider[typeof(TBlock).Name];
        public override string Key => typeof(TBlock).Name.Replace("Block", "").ToLowerInvariant();
    }
}
