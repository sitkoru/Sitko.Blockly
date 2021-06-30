using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Sitko.Blockly.Demo.Data.Entities;
using Sitko.Blockly.Validation;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.Repository;

namespace Sitko.Blockly.Demo.Pages
{
    public class PostForm : BaseRepositoryForm<Post, Guid>
    {
        public string Title { get; set; } = "";
        public List<ContentBlock> Blocks { get; set; } = new();
        public List<ContentBlock> SecondaryBlocks { get; set; } = new();

        public PostForm(IRepository<Post, Guid> repository, ILogger<PostForm> logger) : base(repository, logger)
        {
        }

        protected override Task MapEntityAsync(Post entity)
        {
            entity.Title = Title;
            entity.Blocks = Blocks;
            entity.SecondaryBlocks = SecondaryBlocks;
            return Task.CompletedTask;
        }

        protected override Task MapFormAsync(Post entity)
        {
            Title = entity.Title;
            Blocks = entity.Blocks;
            SecondaryBlocks = entity.SecondaryBlocks;
            return Task.CompletedTask;
        }

        // public List<Type> AllowedBlocks => new() {typeof(TextBlock)};
    }

    public class PostFormValidator : AbstractBlocklyFormValidator<PostForm>
    {
        public PostFormValidator(IEnumerable<IBlockDescriptor> blockDescriptors,
            IEnumerable<IBlockValidator> validators) : base(blockDescriptors, validators)
        {
            AddBlocksValidators(form => form.Blocks);
            RuleFor(f => f.Title).NotEmpty();
        }
    }
}
