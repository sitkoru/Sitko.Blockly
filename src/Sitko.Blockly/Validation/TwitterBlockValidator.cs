using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TwitterBlockValidator : AbstractValidator<TwitterBlock>
    {
        public TwitterBlockValidator()
        {
            RuleFor(d => d.Url).NotEmpty().WithMessage("Укажите ссылку на твит").When(b => b.Enabled);
            RuleFor(d => d.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(block => !string.IsNullOrEmpty(block.Url)).WithMessage("Значение должно быть ссылкой");
        }
    }
}
