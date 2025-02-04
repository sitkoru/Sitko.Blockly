using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.List;

namespace Sitko.EditorJS.Validation;

public class ListBlockValidator : BlockValidator<ListBlock>
{
    public ListBlockValidator(IStringLocalizer<ListBlock> localizer) : base(localizer) =>
        RuleFor(b => b.Data.Items).NotEmpty().WithMessage(Localizer[ValidatorConst.ContentIsRequired]);
}
