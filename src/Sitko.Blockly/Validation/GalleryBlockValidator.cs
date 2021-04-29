using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class GalleryBlockValidator : AbstractValidator<GalleryBlock>
    {
        public GalleryBlockValidator()
        {
            RuleFor(d => d.Pictures).NotEmpty().WithMessage("Выберите картинки");
        }
    }
}