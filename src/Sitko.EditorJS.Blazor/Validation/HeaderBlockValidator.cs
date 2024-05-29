using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.Header;

namespace Sitko.EditorJS.Blazor.Validation;

public class HeaderBlockValidator  : BlockValidator<HeaderBlock>
{

    public HeaderBlockValidator(ILocalizationProvider<HeaderBlock> localizationProvider) : base(localizationProvider) =>
        RuleFor(b => b.Data.Text).NotEmpty().WithMessage(LocalizationProvider[ValidatorConst.TextIsRequired]);
}
