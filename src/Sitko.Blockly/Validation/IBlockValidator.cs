using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Sitko.Blockly.Validation
{
    public interface IBlockValidator : IValidator
    {
    }

    public interface IBlockValidator<in TBlock> : IBlockValidator, IValidator<TBlock> where TBlock : ContentBlock
    {
    }

    public abstract class BlockValidator<TBlock> : AbstractValidator<TBlock>, IBlockValidator<TBlock>
        where TBlock : ContentBlock
    {
        protected readonly IStringLocalizer<TBlock>? _stringLocalizer;

        protected string Localize(string message)
        {
            return _stringLocalizer is null ? message : _stringLocalizer[message]!;
        }

        public BlockValidator(IStringLocalizer<TBlock>? stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
        }
    }
}
