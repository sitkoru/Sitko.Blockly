using System;
using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class YoutubeBlockValidator : BlockValidator<YoutubeBlock>
    {
        public YoutubeBlockValidator(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
            localizationProvider)
        {
            RuleFor(d => d.Url).NotEmpty().WithMessage(LocalizationProvider["Url is required"]).When(b => b.Enabled);
            RuleFor(d => d.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(block => !string.IsNullOrEmpty(block.Url))
                .WithMessage(LocalizationProvider["Value must be valid url"]);
        }
    }
}
