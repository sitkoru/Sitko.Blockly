using Microsoft.AspNetCore.Mvc;
using Sitko.Blockly.Data;
using Sitko.Core.Storage;
using Sitko.Core.Storage.Remote.Server;

namespace Sitko.Blockly.Demo.Controllers;

[Route("/Upload")]
public class FileStorageController(
    IStorage<BlocklyStorageOptions> storage,
    ILogger<FileStorageController> logger)
    : BaseRemoteStorageController<BlocklyStorageOptions, TestMetadata>(storage, logger);
