using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.Quote;

namespace Sitko.EditorJS.Blazor.Validation;

public class QuoteBlockValidator : BlockValidator<QuoteBlock>
{
    public QuoteBlockValidator(IStringLocalizer<QuoteBlock> localizer) : base(localizer)
    {
        RuleFor(b => b.Data.Text).NotEmpty().WithMessage(Localizer[ValidatorConst.TextIsRequired]);
        RuleFor(b => b.Data.Caption).NotEmpty().WithMessage(Localizer[ValidatorConst.CaptionIsRequired]);
    }
}
