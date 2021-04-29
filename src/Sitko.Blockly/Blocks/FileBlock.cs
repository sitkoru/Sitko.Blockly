using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks
{
    public record FileBlock : ContentBlock
    {
        public override string ToString()
        {
            return File is null ? "Файл не выбран" : $"Файл: {File.FileName}";
        }

        public StorageItem? File { get; set; }
    }
}
