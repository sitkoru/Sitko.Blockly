using System;
using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class QuoteBlockValidator : BlockValidator<QuoteBlock>
    {
        public QuoteBlockValidator(ILocalizationProvider<QuoteBlock> localizationProvider) : base(localizationProvider)
        {
            RuleFor(d => d.Text).NotEmpty().WithMessage(LocalizationProvider["Text is required"]).When(b => b.Enabled);
            RuleFor(p => p.Link).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Link)).WithMessage(LocalizationProvider["Value must be valid url"]);
        }
    }
}
