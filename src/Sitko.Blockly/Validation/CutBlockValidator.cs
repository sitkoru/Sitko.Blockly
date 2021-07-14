using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class CutBlockValidator : BlockValidator<CutBlock>
    {
        public CutBlockValidator(ILocalizationProvider<CutBlock> localizationProvider) : base(localizationProvider) =>
            RuleFor(d => d.ButtonText).NotEmpty().WithMessage(LocalizationProvider["Button text is required"])
                .When(b => b.Enabled);
    }
}
