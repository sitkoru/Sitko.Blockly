using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.Gallery;

namespace Sitko.EditorJS.Blazor.Validation;

public class GalleryBlockValidator<TImageData> : BlockValidator<GalleryBlock<TImageData>>
    where TImageData: class, new()
{
    public GalleryBlockValidator(ILocalizationProvider<GalleryBlock<TImageData>> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.Files).NotEmpty().WithMessage(LocalizationProvider["Choose at least 1 picture"]);
}
