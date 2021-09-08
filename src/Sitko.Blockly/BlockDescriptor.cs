using System;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly
{
    public interface IBlockDescriptor
    {
        string Title { get; }
        Type Type { get; }
        string Key { get; }
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
    }

    public abstract record BlockDescriptor<TBlock> : BlockDescriptor, IBlockDescriptor<TBlock>
        where TBlock : ContentBlock
    {
        protected ILocalizationProvider<TBlock> LocalizationProvider { get; }

        public BlockDescriptor(ILocalizationProvider<TBlock> localizationProvider) => LocalizationProvider = localizationProvider;

        public override Type Type => typeof(TBlock);

        public override string Title => LocalizationProvider[typeof(TBlock).Name];
        public override string Key => typeof(TBlock).Name.Replace("Block", "").ToLowerInvariant();
    }
}
