using System;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class QuoteBlockValidator : BlockValidator<QuoteBlock>
    {
        public QuoteBlockValidator(IStringLocalizer<QuoteBlock>? stringLocalizer = null) : base(stringLocalizer)
        {
            RuleFor(d => d.Text).NotEmpty().WithMessage(Localize("Text is required")).When(b => b.Enabled);
            RuleFor(p => p.Link).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Link)).WithMessage(Localize("Value must be valid url"));
        }
    }
}
