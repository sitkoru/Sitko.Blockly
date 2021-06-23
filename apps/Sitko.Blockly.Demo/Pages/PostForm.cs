using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitko.Blockly.Demo.Data.Entities;
using Sitko.Blockly.Validation;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.Repository;

namespace Sitko.Blockly.Demo.Pages
{
    public class PostForm : BaseRepositoryForm<Post, Guid>, IBlocklyForm
    {
        public List<ContentBlock> Blocks { get; set; }

        public PostForm(IRepository<Post, Guid> repository, ILogger<PostForm> logger) : base(repository, logger)
        {
        }

        protected override Task MapEntityAsync(Post entity)
        {
            entity.Blocks = Blocks;
            return Task.CompletedTask;
        }

        protected override Task MapFormAsync(Post entity)
        {
            Blocks = entity.Blocks;
            return Task.CompletedTask;
        }

        // public List<Type> AllowedBlocks => new() {typeof(TextBlock)};
    }

    public class PostFormValidator : AbstractBlocklyFormValidator<PostForm>
    {
    }
}
