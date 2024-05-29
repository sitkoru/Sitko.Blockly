using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.List;

namespace Sitko.EditorJS.Blazor.Validation;

public class ListBlockValidator : BlockValidator<ListBlock>
{
    public ListBlockValidator(ILocalizationProvider<ListBlock> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.Items).NotEmpty().WithMessage(LocalizationProvider[ValidatorConst.ContentIsRequired]);
}
