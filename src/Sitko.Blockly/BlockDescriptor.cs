using System;
using Microsoft.Extensions.Localization;

namespace Sitko.Blockly
{
    public interface IBlockDescriptor
    {
        string Title { get; }
        Type Type { get; }
    }

    public interface IBlockDescriptor<TBlock> : IBlockDescriptor where TBlock : ContentBlock
    {
    }

    public abstract record BlockDescriptor : IBlockDescriptor
    {
        public abstract string Title { get; }
        public abstract Type Type { get; }
    }

    public abstract record BlockDescriptor<TBlock> : BlockDescriptor, IBlockDescriptor<TBlock>
        where TBlock : ContentBlock
    {
        protected IStringLocalizer<TBlock>? Localizer { get; }

        public BlockDescriptor(IStringLocalizer<TBlock>? localizer = null)
        {
            Localizer = localizer;
        }

        public override Type Type => typeof(TBlock);

        public override string Title => Localizer is null ? typeof(TBlock).Name! : Localizer[typeof(TBlock).Name]!;
    }
}
