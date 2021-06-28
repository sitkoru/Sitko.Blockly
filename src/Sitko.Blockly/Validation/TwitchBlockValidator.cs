using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TwitchBlockValidator : AbstractValidator<TwitchBlock>
    {
        public TwitchBlockValidator()
        {
            RuleFor(d => d.Url).NotEmpty().WithMessage("Url is required").When(b => b.Enabled);
            RuleFor(d => d.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(b => !string.IsNullOrEmpty(b.Url)).WithMessage("Value must be valid url");
        }
    }
}
