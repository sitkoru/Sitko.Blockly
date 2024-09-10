using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.Header;

namespace Sitko.EditorJS.Blazor.Validation;

public class HeaderBlockValidator  : BlockValidator<HeaderBlock>
{

    public HeaderBlockValidator(IStringLocalizer<HeaderBlock> localizer) : base(localizer) =>
        RuleFor(b => b.Data.Text).NotEmpty().WithMessage(Localizer[ValidatorConst.TextIsRequired]);
}
