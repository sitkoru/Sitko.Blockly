using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.Quote;

namespace Sitko.EditorJS.Blazor.Validation;

public class QuoteBlockValidator : BlockValidator<QuoteBlock>
{
    public QuoteBlockValidator(ILocalizationProvider<QuoteBlock> localizationProvider) : base(localizationProvider)
    {
        RuleFor(b => b.Data.Text).NotEmpty().WithMessage(LocalizationProvider["Text is required"]);
        RuleFor(b => b.Data.Caption).NotEmpty().WithMessage(LocalizationProvider["Caption is required"]);
    }
}
