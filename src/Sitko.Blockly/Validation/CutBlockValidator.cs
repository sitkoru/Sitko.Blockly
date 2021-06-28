﻿using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class CutBlockValidator : AbstractValidator<CutBlock>
    {
        public CutBlockValidator()
        {
            RuleFor(d => d.ButtonText).NotEmpty().WithMessage("Укажите текст кнопки").When(b => b.Enabled);
        }
    }
}
