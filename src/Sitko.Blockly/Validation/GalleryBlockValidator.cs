using FluentValidation;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Validation
{
    public class GalleryBlockValidator : BlockValidator<GalleryBlock>
    {
        public GalleryBlockValidator(ILocalizationProvider<GalleryBlock> localizationProvider) : base(
            localizationProvider)
        {
            RuleFor(d => d.Pictures).NotEmpty().WithMessage(LocalizationProvider["Choose at least 1 picture"])
                .When(b => b.Enabled);
        }
    }
}
