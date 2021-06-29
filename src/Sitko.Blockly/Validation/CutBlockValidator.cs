using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class CutBlockValidator : BlockValidator<CutBlock>
    {
        public CutBlockValidator(IStringLocalizer<CutBlock>? localizer = null) : base(localizer)
        {
            RuleFor(d => d.ButtonText).NotEmpty().WithMessage(Localize("Button text is required")).When(b => b.Enabled);
        }
    }
}
