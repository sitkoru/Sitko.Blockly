using System;
using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class PictureBlockValidator : AbstractValidator<PictureBlock>
    {
        public PictureBlockValidator()
        {
            RuleFor(p => p.Picture).NotNull().WithMessage("Выберите картину");
            RuleFor(p => p.Url).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.Url)).WithMessage("Значение должно быть ссылкой");
        }
    }
}