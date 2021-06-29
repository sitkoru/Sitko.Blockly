using System;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class IframeBlockValidator : BlockValidator<IframeBlock>
    {
        public IframeBlockValidator(IStringLocalizer<IframeBlock>? stringLocalizer = null) : base(stringLocalizer)
        {
            RuleFor(p => p.Src).NotEmpty().WithMessage(Localize("Source url is required")).When(b => b.Enabled);
            RuleFor(p => p.Src).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Src))
                .WithMessage(Localize("Value must be valid url"));
        }
    }
}
