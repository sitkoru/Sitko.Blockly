using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation;

public class TextBlockValidator : BlockValidator<TextBlock>
{
    public TextBlockValidator(ILocalizationProvider<TextBlock> localizationProvider) : base(localizationProvider) =>
        RuleFor(p => p.Text).NotEmpty().WithMessage(LocalizationProvider["Text is required"]).When(b => b.Enabled);
}
