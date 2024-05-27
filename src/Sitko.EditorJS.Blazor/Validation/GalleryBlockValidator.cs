using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.Core.Storage;
using Sitko.EditorJS.Blocks.Gallery;

namespace Sitko.EditorJS.Blazor.Validation;

public class GalleryBlockValidator : BlockValidator<GalleryBlock<StorageItem>>
{

    public GalleryBlockValidator(ILocalizationProvider<GalleryBlock<StorageItem>> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.Files).NotEmpty().WithMessage(LocalizationProvider["Choose at least 1 picture"]);
}
