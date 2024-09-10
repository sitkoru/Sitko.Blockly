using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blazor.Validation;

public class ImageBlockValidator<TImageData> : BlockValidator<ImageBlock<TImageData>>
    where TImageData: class, new()
{
    public ImageBlockValidator(IStringLocalizer<ImageBlock<TImageData>> localizer) : base(localizer) =>
        RuleFor(b => b.Data.File).NotEmpty().WithMessage(Localizer[ValidatorConst.ChoosePicture]);
}
