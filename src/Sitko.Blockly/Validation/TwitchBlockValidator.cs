using System;
using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class TwitchBlockValidator : BlockValidator<TwitchBlock>
    {
        public TwitchBlockValidator(ILocalizationProvider<TwitchBlock> localizationProvider) : base(localizationProvider)
        {
            RuleFor(d => d.Url).NotEmpty().WithMessage(LocalizationProvider["Url is required"]).When(b => b.Enabled);
            RuleFor(d => d.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(b => !string.IsNullOrEmpty(b.Url)).WithMessage(LocalizationProvider["Value must be valid url"]);
        }
    }
}
