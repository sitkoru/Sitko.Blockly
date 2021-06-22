using Sitko.Core.Storage;

namespace Sitko.Blockly.Blocks
{
    public record PictureBlock : ContentBlock
    {
        public override string ToString()
        {
            return Picture is null ? "Картинка не выбрана" : $"Картинка: {Picture.FileName}";
        }

        public StorageItem? Picture { get; set; }
        public string? Url { get; set; }
    }
}
