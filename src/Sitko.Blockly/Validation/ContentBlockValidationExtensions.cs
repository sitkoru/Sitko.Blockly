using System;
using System.Collections.Generic;
using System.Linq;
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
            var validatorsArray = validators.ToArray();
            var additionalValidatorsArray = additionalValidators?.ToArray();
            foreach (var descriptor in blockDescriptors)
            {
                validator.Add(descriptor, validatorsArray);
                if (additionalValidatorsArray is not null)
                {
                    foreach (var additionalValidator in additionalValidatorsArray)
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
        private readonly IEnumerable<IBlockDescriptor> blockDescriptors;
        private readonly IEnumerable<IBlockValidator> validators;

        public AbstractBlocklyFormValidator(IEnumerable<IBlockDescriptor> blockDescriptors,
            IEnumerable<IBlockValidator> validators)
        {
            this.blockDescriptors = blockDescriptors;
            this.validators = validators;
        }

        protected AbstractBlocklyFormValidator<TForm> AddBlocksValidators(
            Expression<Func<TForm, IEnumerable<ContentBlock>>> fieldSelector)
        {
            RuleForEach(fieldSelector)
                .AddBlockValidators(blockDescriptors, validators, AdditionalValidators);
            return this;
        }

        protected virtual IEnumerable<AbstractValidator<ContentBlock>>? AdditionalValidators => null;
    }
}
