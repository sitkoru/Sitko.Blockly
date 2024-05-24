namespace Sitko.EditorJS.Blocks.Image;

public record ImageBlockConfig : ContentBlockConfig
{
    public string Types { get; set; } = "image/*";

    public string CaptionPlaceholder { get; set; } = "Caption";

    public string? ButtonContent { get; set; }
    public string ByFileUploadUrl { get; set; } = "/upload/file";
    public string ByUrlUploadUrl { get; set; } = "/upload/url";

    // [JsonPropertyName("uploader")]
    // public ImageBlockUploader Uploader { get; set; } = new();
}
