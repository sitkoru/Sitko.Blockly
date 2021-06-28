using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class CutBlockValidator : AbstractValidator<CutBlock>
    {
        public CutBlockValidator()
        {
            RuleFor(d => d.ButtonText).NotEmpty().WithMessage("Button text is required").When(b => b.Enabled);
        }
    }
}
