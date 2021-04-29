using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class YoutubeBlockValidator : AbstractValidator<YoutubeBlock>
    {
        public YoutubeBlockValidator()
        {
            RuleFor(d => d.YoutubeLink).NotEmpty().WithMessage("Укажите ссылку на видео");
        }
    }
}