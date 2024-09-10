using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.Table;

namespace Sitko.EditorJS.Blazor.Validation;

public class TableBlockValidator : BlockValidator<TableBlock>
{
    public TableBlockValidator(IStringLocalizer<TableBlock> localizer) : base(localizer) =>
        RuleFor(b => b.Data.Content).NotEmpty().WithMessage(Localizer[ValidatorConst.ContentIsRequired]);
}
