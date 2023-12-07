using FluentValidation;
using KellermanSoftware.CompareNetObjects;
using Sitko.Blockly.Blazor.Extensions;
using Sitko.Blockly.Data.Entities;
using Sitko.Blockly.Validation;
using Sitko.Core.Blazor.MudBlazorComponents;
using Sitko.Core.Repository;

namespace Sitko.Blockly.Demo.Client.Pages;

public class PostForm : BaseMudRepositoryForm<Post, Guid, IRepository<Post, Guid>>
{
    protected override void ConfigureComparer(ComparisonConfig comparisonConfig)
    {
        base.ConfigureComparer(comparisonConfig);
        comparisonConfig.AddBlocklyCollectionMapping();
    }
}

public class PostFormValidator : AbstractBlocklyFormValidator<Post>
{
    public PostFormValidator(IEnumerable<IBlockDescriptor> blockDescriptors,
        IEnumerable<IBlockValidator> validators) : base(blockDescriptors, validators) =>
        RuleFor(f => f.Title).NotEmpty();
}
