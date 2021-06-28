using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TextBlockValidator : AbstractValidator<TextBlock>
    {
        public TextBlockValidator()
        {
            RuleFor(p => p.Text).NotEmpty().WithMessage("Text is required").When(b => b.Enabled);
        }
    }
}
