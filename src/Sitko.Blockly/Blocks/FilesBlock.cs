using Sitko.Core.App.Collections;
using Sitko.Core.App.Localization;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks;

[ContentBlockMetadata(5)]
public record FilesBlock : ContentBlock
{
    public ValueCollection<StorageItem> Files { get; set; } = new();
    public override string ToString() => $"Files: {string.Join(", ", Files.Select(p => p.FileName))}";
}

public record FilesBlockDescriptor : BlockDescriptor<FilesBlock>
{
    public FilesBlockDescriptor(ILocalizationProvider<FilesBlock> localizationProvider) : base(localizationProvider)
    {
    }
}
