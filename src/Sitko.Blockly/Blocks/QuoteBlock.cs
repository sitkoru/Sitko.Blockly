using System;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks
{
    public record QuoteBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"{Author}: {Text} ({Link})";
        }

        public string Text { get; set; } = string.Empty;
        public string? Author { get; set; }
        public string? Link { get; set; }
        public StorageItem? Picture { get; set; }
    }
}
