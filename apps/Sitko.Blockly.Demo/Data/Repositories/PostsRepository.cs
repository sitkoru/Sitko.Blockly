using System;
using Sitko.Blockly.Demo.Data.Entities;
using Sitko.Core.Repository.EntityFrameworkCore;

namespace Sitko.Blockly.Demo.Data.Repositories;

public class PostsRepository : EFRepository<Post, Guid, BlocklyContext>
{
    public PostsRepository(EFRepositoryContext<Post, Guid, BlocklyContext> repositoryContext) : base(repositoryContext)
    {
    }
}
