using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class GalleryBlockValidator : BlockValidator<GalleryBlock>
    {
        public GalleryBlockValidator(IStringLocalizer<GalleryBlock>? stringLocalizer = null) : base(stringLocalizer)
        {
            RuleFor(d => d.Pictures).NotEmpty().WithMessage(Localize("Choose at least 1 picture")).When(b => b.Enabled);
        }
    }
    
    public class FilesBlockValidator : BlockValidator<FilesBlock>
    {
        public FilesBlockValidator(IStringLocalizer<FilesBlock>? stringLocalizer = null) : base(stringLocalizer)
        {
            RuleFor(d => d.Files).NotEmpty().WithMessage(Localize("Choose at least 1 file")).When(b => b.Enabled);
        }
    }
}
