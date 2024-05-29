using FluentValidation;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blocks.Paragraph;

namespace Sitko.EditorJS.Blazor.Validation;

public class ParagraphBlockValidator : BlockValidator<ParagraphBlock>
{
    public ParagraphBlockValidator(ILocalizationProvider<ParagraphBlock> localizationProvider) : base(localizationProvider) =>
        RuleFor(p => p.Data.Text).NotEmpty().WithMessage(LocalizationProvider[ValidatorConst.TextIsRequired]);
}
