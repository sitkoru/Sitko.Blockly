using FluentValidation;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Validation
{
    public class FileBlockValidator : AbstractValidator<FileBlock>
    {
        public FileBlockValidator()
        {
            RuleFor(d => d.File).NotNull().WithMessage("Выберите файл");
        }
    }
}