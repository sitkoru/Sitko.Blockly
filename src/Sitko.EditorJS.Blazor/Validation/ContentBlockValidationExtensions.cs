using FluentValidation;
using FluentValidation.Validators;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Validation;

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
