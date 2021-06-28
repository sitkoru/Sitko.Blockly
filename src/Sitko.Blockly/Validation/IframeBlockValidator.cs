using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class IframeBlockValidator : AbstractValidator<IframeBlock>
    {
        public IframeBlockValidator()
        {
            RuleFor(p => p.Src).NotEmpty().WithMessage("Source url is required").When(b => b.Enabled);
            RuleFor(p => p.Src).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Value must be valid url");
        }
    }
}
