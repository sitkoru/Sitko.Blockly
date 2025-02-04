using FluentValidation;
using Microsoft.Extensions.Localization;
using Sitko.EditorJS.Blocks.Paragraph;

namespace Sitko.EditorJS.Validation;

public class ParagraphBlockValidator : BlockValidator<ParagraphBlock>
{
    public ParagraphBlockValidator(IStringLocalizer<ParagraphBlock> localizer) : base(localizer) =>
        RuleFor(p => p.Data.Text).NotEmpty().WithMessage(Localizer[ValidatorConst.TextIsRequired]);
}
