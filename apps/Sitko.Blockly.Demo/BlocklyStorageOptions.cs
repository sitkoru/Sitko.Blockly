using Sitko.Core.Storage;
using Sitko.Core.Storage.FileSystem;

namespace Sitko.Blockly.Demo;

public class BlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
{
    public string StoragePath { get; set; } = "";
}
