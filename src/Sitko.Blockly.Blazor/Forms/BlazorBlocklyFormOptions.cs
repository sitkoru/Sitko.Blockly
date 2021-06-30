namespace Sitko.Blockly.Blazor.Forms
{
    public class BlazorBlocklyFormOptions : BlocklyFormOptions
    {
        public BlockFormStorageOptions FilesOptions { get; set; } = new();

        public BlockFormStorageOptions ImagesOptions { get; set; } =
            new() {AllowedTypes = "image/jpeg,image/png,image/svg+xml"};
    }
}
