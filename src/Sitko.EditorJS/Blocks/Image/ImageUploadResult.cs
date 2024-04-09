using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.Image;

public record ImageUploadResult
{
    [JsonPropertyName("success")] public int Success { get; protected init; }
    public static ImageUploadResult Failed() => new() { Success = 0 };

    public static ImageUploadResult Ok<TData>(ImageBlockDataFile<TData> data) where TData : class, new() =>
        new ImageUploadResult<TData>() { Success = 1, File = data };
}

public record ImageUploadResult<TData> : ImageUploadResult where TData : class, new()
{
    [JsonPropertyName("file")] public ImageBlockDataFile<TData> File { get; internal init; }
}
