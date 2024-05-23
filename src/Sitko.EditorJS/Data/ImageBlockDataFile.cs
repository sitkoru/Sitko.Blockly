using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Data;

public record ImageBlockDataFile<TData> where TData : class, new()
{
    [JsonPropertyName("url")] public string Url { get; set; } = "";
    [JsonPropertyName("data")] public TData Data { get; set; } = new();
}
