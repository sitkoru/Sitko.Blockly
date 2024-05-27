using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.Core.Storage;
using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Blazor.Validation;

public class ImageBlockValidator : BlockValidator<ImageBlock<StorageItem>>
{

    public ImageBlockValidator(ILocalizationProvider<ImageBlock<StorageItem>> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.File).NotEmpty().WithMessage(LocalizationProvider["Choose a picture"]);
}
