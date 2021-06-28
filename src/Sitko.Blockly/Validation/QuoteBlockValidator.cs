using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class QuoteBlockValidator : AbstractValidator<QuoteBlock>
    {
        public QuoteBlockValidator()
        {
            RuleFor(d => d.Text).NotEmpty().WithMessage("Text is required").When(b => b.Enabled);
            RuleFor(p => p.Link).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Link)).WithMessage("Value must be valid url");
        }
    }
}
