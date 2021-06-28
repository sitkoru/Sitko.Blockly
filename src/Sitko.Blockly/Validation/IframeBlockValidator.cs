using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class IframeBlockValidator : AbstractValidator<IframeBlock>
    {
        public IframeBlockValidator()
        {
            RuleFor(p => p.Src).NotEmpty().WithMessage("Укажите ссылку").When(b => b.Enabled);
            RuleFor(p => p.Src).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .WithMessage("Значение должно быть ссылкой");
        }
    }
}
