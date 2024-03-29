﻿using Sitko.Core.App.Collections;
using Sitko.Core.App.Localization;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks;

[ContentBlockMetadata(3)]
public record GalleryBlock : ContentBlock
{
    public ValueCollection<StorageItem> Pictures { get; set; } = new();
    public override string ToString() => $"Gallery: {string.Join(", ", Pictures.Select(p => p.FileName))}";
}

public record GalleryBlockDescriptor : BlockDescriptor<GalleryBlock>
{
    public GalleryBlockDescriptor(ILocalizationProvider<GalleryBlock> localizationProvider) : base(
        localizationProvider)
    {
    }
}
