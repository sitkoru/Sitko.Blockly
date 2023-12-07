using Sitko.Blockly.Data.Entities;
using Sitko.Core.Repository.Remote;

namespace Sitko.Blockly.Demo.Client.Data.Repositories;

public class PostRemoteRepository : BaseRemoteRepository<Post, Guid>
{
    public PostRemoteRepository(RemoteRepositoryContext<Post, Guid> repositoryContext) : base(repositoryContext)
    {
    }
}
