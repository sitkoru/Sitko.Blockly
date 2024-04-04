using System.Text.Json.Serialization;
using Sitko.EditorJS.Json;

namespace Sitko.EditorJS.Blocks;

[JsonConverter(typeof(ContentBlockConverter))]
public record ContentBlock : IContentBlock
{
    [JsonPropertyName("id")] public required string Id { get; init; } = "";
}

public record ContentBlock<TData>
    : ContentBlock where TData : ContentBlockData, new()
{
    [JsonPropertyName("data")] public required TData Data { get; init; }
}
