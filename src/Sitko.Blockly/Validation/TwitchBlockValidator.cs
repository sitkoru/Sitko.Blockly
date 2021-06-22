using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TwitchBlockValidator : AbstractValidator<TwitchBlock>
    {
        public TwitchBlockValidator()
        {
            RuleFor(d => d.Url).NotEmpty().WithMessage("Укажите ссылку на видео");
            RuleFor(d => d.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(b => !string.IsNullOrEmpty(b.Url)).WithMessage("Значение должно быть ссылкой");
        }
    }
}
