using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TwitterBlockValidator : AbstractValidator<TwitterBlock>
    {
        public TwitterBlockValidator()
        {
            RuleFor(d => d.TweetLink).NotEmpty().WithMessage("Укажите ссылку на твит")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Значение должно быть ссылкой");
        }
    }
}