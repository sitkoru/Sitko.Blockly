using System.Linq;
using Sitko.Core.App.Collections;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks
{
    public record FilesBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"Files: {string.Join(", ", Files.Select(p => p.FileName))}";
        }

        public ValueCollection<StorageItem> Files { get; set; } = new();
    }
}
