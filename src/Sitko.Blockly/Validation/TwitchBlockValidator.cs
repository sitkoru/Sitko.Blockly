using System;
using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TwitchBlockValidator : BlockValidator<TwitchBlock>
    {
        public TwitchBlockValidator(IStringLocalizer<TwitchBlock>? stringLocalizer = null) : base(stringLocalizer)
        {
            RuleFor(d => d.Url).NotEmpty().WithMessage(Localize("Url is required")).When(b => b.Enabled);
            RuleFor(d => d.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(b => !string.IsNullOrEmpty(b.Url)).WithMessage(Localize("Value must be valid url"));
        }
    }
}
