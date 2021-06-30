using System.Linq;
using Sitko.Core.App.Collections;
using Sitko.Core.App.Localization;
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
        public override int Priority => 5;

        public FilesBlockDescriptor(ILocalizationProvider<FilesBlock> localizationProvider) : base(localizationProvider)
        {
        }
    }
}
