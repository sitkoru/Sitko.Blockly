using System.Linq;
using Microsoft.Extensions.Localization;
using Sitko.Core.App.Collections;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks
{
    public record GalleryBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"Gallery: {string.Join(", ", Pictures.Select(p => p.FileName))}";
        }

        public ValueCollection<StorageItem> Pictures { get; set; } = new();
    }
    
    public record GalleryBlockDescriptor : BlockDescriptor<GalleryBlock>
    {
        public GalleryBlockDescriptor(IStringLocalizer<GalleryBlock>? localizer = null) : base(localizer)
        {
        }
    }
}
