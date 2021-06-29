using System.Linq;
using Microsoft.Extensions.Localization;
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
    
    public record FilesBlockDescriptor : BlockDescriptor<FilesBlock>
    {
        public FilesBlockDescriptor(IStringLocalizer<FilesBlock>? localizer = null) : base(localizer)
        {
        }
    }
}
