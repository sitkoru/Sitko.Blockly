using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FluentValidation;
using FluentValidation.Validators;

namespace Sitko.Blockly.Validation
{
    public static class ContentBlockValidationExtensions
    {
        public static IRuleBuilderOptions<TModel, ContentBlock> AddBlockValidators<TModel>(
            this IRuleBuilderInitialCollection<TModel, ContentBlock> options,
            IEnumerable<IBlockDescriptor> blockDescriptors, IEnumerable<IBlockValidator> validators,
            IEnumerable<AbstractValidator<ContentBlock>>? additionalValidators = null
        )
        {
            var validator = new BlockInheritanceValidator<TModel>();
            foreach (var descriptor in blockDescriptors)
            {
                validator.Add(descriptor, validators);
                if (additionalValidators is not null)
                {
                    foreach (var additionalValidator in additionalValidators)
                    {
                        validator.Add(additionalValidator);
                    }
                }
            }

            return options.SetValidator(validator);
        }
    }

    public class BlockInheritanceValidator<TModel> : PolymorphicValidator<TModel, ContentBlock>
    {
        public BlockInheritanceValidator<TModel> Add(IBlockDescriptor descriptor,
            IEnumerable<IBlockValidator> validators)
        {
            foreach (var blockValidator in validators)
            {
                if (blockValidator.CanValidateInstancesOfType(descriptor.Type))
                {
                    Add(descriptor.Type, blockValidator);
                }
            }

            return this;
        }
    }

    public abstract class AbstractBlocklyFormValidator<TForm> : AbstractValidator<TForm>
    {
        private readonly IEnumerable<IBlockDescriptor> _blockDescriptors;
        private readonly IEnumerable<IBlockValidator> _validators;

        public AbstractBlocklyFormValidator(IEnumerable<IBlockDescriptor> blockDescriptors,
            IEnumerable<IBlockValidator> validators)
        {
            _blockDescriptors = blockDescriptors;
            _validators = validators;
        }

        protected AbstractBlocklyFormValidator<TForm> AddBlocksValidators(
            Expression<Func<TForm, IEnumerable<ContentBlock>>> fieldSelector)
        {
            RuleForEach(fieldSelector)
                .AddBlockValidators(_blockDescriptors, _validators, AdditionalValidators);
            return this;
        }

        protected virtual IEnumerable<AbstractValidator<ContentBlock>>? AdditionalValidators => null;
    }
}
