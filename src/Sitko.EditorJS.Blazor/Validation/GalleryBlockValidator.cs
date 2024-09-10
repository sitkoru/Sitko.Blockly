using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.Gallery;

namespace Sitko.EditorJS.Blazor.Validation;

public class GalleryBlockValidator<TImageData> : BlockValidator<GalleryBlock<TImageData>>
    where TImageData: class, new()
{
    public GalleryBlockValidator(IStringLocalizer<GalleryBlock<TImageData>> localizer) : base(localizer) =>
        RuleFor(b => b.Data.Files).NotEmpty().WithMessage(Localizer[ValidatorConst.ChooseMorePicture]);
}
