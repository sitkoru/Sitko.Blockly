using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation;

public class FilesBlockValidator : BlockValidator<FilesBlock>
{
    public FilesBlockValidator(ILocalizationProvider<FilesBlock> localizationProvider) : base(localizationProvider) =>
        RuleFor(d => d.Files).NotEmpty().WithMessage(LocalizationProvider["Choose at least 1 file"])
            .When(b => b.Enabled);
}
