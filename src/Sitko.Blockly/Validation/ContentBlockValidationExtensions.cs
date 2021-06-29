using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.Localization;

namespace Sitko.Blockly.Validation
{
    public static class ContentBlockValidationExtensions
    {
        public static IRuleBuilderOptions<TModel, ContentBlock> AddBlockValidators<TModel>(
            this IRuleBuilderInitialCollection<TModel, ContentBlock> options,
            IEnumerable<IBlockDescriptor> blockDescriptors, IEnumerable<IBlockValidator> validators,
            IEnumerable<AbstractValidator<ContentBlock>>? additionalValidators = null
        ) where TModel : IBlocklyEntity
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

        private static IStringLocalizer<TBlock>? GetLocalizer<TBlock>(IStringLocalizerFactory? factory)
            where TBlock : ContentBlock
        {
            if (factory is null)
            {
                return null;
            }

            var localizer = factory.Create(typeof(TBlock));
            if (localizer is IStringLocalizer<TBlock> typedLocalizer)
            {
                return typedLocalizer;
            }

            return null;
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

    public abstract class AbstractBlocklyFormValidator<TForm> : AbstractValidator<TForm> where TForm : IBlocklyForm
    {
        public AbstractBlocklyFormValidator(IEnumerable<IBlockDescriptor> blockDescriptors,
            IEnumerable<IBlockValidator> validators)
        {
            RuleForEach(f => f.Blocks).AddBlockValidators(blockDescriptors, validators, AdditionalValidators);
        }

        protected virtual IEnumerable<AbstractValidator<ContentBlock>>? AdditionalValidators => null;
    }
}
