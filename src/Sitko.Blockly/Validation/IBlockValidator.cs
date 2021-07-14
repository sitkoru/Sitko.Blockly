using FluentValidation;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public interface IBlockValidator : IValidator
    {
    }

    public interface IBlockValidator<in TBlock> : IBlockValidator, IValidator<TBlock> where TBlock : ContentBlock
    {
    }

    public abstract class BlockValidator<TBlock> : AbstractValidator<TBlock>, IBlockValidator<TBlock>
        where TBlock : ContentBlock
    {
        protected ILocalizationProvider<TBlock> LocalizationProvider { get; }

        protected BlockValidator(ILocalizationProvider<TBlock> localizationProvider) =>
            LocalizationProvider = localizationProvider;
    }
}
