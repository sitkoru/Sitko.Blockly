using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TwitchBlockValidator : AbstractValidator<TwitchBlock>
    {
        public TwitchBlockValidator()
        {
            RuleFor(d => d.TwitchLink).NotEmpty().WithMessage("Укажите ссылку на видео")
                .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).WithMessage("Значение должно быть ссылкой");
        }
    }
}