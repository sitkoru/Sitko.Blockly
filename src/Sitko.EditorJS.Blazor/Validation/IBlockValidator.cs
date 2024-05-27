using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Validation;

public interface IBlockValidator : IValidator
{
}

public interface IBlockValidator<in TBlock> : IBlockValidator, IValidator<TBlock> where TBlock : ContentBlock
{
}

public abstract class BlockValidator<TBlock> : AbstractValidator<TBlock>, IBlockValidator<TBlock>
    where TBlock : ContentBlock
{
    protected BlockValidator(ILocalizationProvider<TBlock> localizationProvider) =>
        LocalizationProvider = localizationProvider;

    protected ILocalizationProvider<TBlock> LocalizationProvider { get; }
}
