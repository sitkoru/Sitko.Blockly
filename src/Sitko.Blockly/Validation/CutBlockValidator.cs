using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class CutBlockValidator : BlockValidator<CutBlock>
    {
        public CutBlockValidator(ILocalizationProvider<CutBlock> localizer) : base(localizer)
        {
            RuleFor(d => d.ButtonText).NotEmpty().WithMessage(LocalizationProvider["Button text is required"])
                .When(b => b.Enabled);
        }
    }
}
