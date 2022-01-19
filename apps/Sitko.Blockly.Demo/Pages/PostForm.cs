using System;
using System.Collections.Generic;
using FluentValidation;
using Sitko.Blockly.Demo.Data.Entities;
using Sitko.Blockly.Validation;
using Sitko.Core.Blazor.Forms;
using Sitko.Core.Blazor.MudBlazorComponents;

namespace Sitko.Blockly.Demo.Pages;

using Blazor.Extensions;
using Data.Repositories;
using KellermanSoftware.CompareNetObjects;
using Microsoft.AspNetCore.Components;

public class PostForm : BaseMudRepositoryForm<Post, Guid, PostsRepository>
{
    [Parameter] public RenderFragment<PostForm> ChildContent { get; set; } = null!;

    protected override RenderFragment ChildContentFragment => ChildContent(this);

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
