using System;
using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class IframeBlockValidator : BlockValidator<IframeBlock>
    {
        public IframeBlockValidator(ILocalizationProvider<IframeBlock> localizationProvider) : base(localizationProvider)
        {
            RuleFor(p => p.Src).NotEmpty().WithMessage(localizationProvider["Source url is required"]).When(b => b.Enabled);
            RuleFor(p => p.Src).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Src))
                .WithMessage(localizationProvider["Value must be valid url"]);
        }
    }
}
