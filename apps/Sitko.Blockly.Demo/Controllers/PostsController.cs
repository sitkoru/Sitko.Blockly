using Microsoft.AspNetCore.Mvc;
using Sitko.Blockly.Data.Entities;
using Sitko.Core.Repository;
using Sitko.Core.Repository.Remote.Server;

namespace Sitko.Blockly.Demo.Controllers;

[Route("/api/Post")]
public class PostsController(IRepository<Post, Guid> repository, ILogger<PostsController> logger)
    : BaseRemoteRepositoryController<Post, Guid>(repository,
        logger);
