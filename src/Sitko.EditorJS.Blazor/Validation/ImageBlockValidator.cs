using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blazor.Validation;

public class ImageBlockValidator<TImageData> : BlockValidator<ImageBlock<TImageData>>
    where TImageData: class, new()
{
    public ImageBlockValidator(ILocalizationProvider<ImageBlock<TImageData>> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.File).NotEmpty().WithMessage(LocalizationProvider[ValidatorConst.ChoosePicture]);
}
