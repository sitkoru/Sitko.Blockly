using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class TextBlockValidator : BlockValidator<TextBlock>
    {
        public TextBlockValidator(IStringLocalizer<TextBlock>? stringLocalizer = null) : base(stringLocalizer)
        {
            RuleFor(p => p.Text).NotEmpty().WithMessage(Localize("Text is required")).When(b => b.Enabled);
        }
    }
}
