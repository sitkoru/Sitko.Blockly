using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.Table;

namespace Sitko.EditorJS.Blazor.Validation;

public class TableBlockValidator : BlockValidator<TableBlock>
{
    public TableBlockValidator(ILocalizationProvider<TableBlock> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.Content).NotEmpty().WithMessage(LocalizationProvider["Content is required"]);
}
