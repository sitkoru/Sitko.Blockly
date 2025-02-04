using System.Text.Json.Serialization;

namespace Sitko.EditorJS.Blocks.List;

public record ListBlockData : ContentBlockData
{
    [JsonPropertyName("style")] public string Style { get; set; } = "";
    [JsonPropertyName("items")] public ListBlockItemData[] Items { get; set; } = [];
}

public record ListBlockItemData
{
    [JsonPropertyName("content")] public string Content { get; set; } = "";
    [JsonPropertyName("meta")] public ListBlockItemMetaData ItemMeta { get; set; } = new();
    [JsonPropertyName("items")] public ListBlockItemData[] Items { get; set; } = [];
}

public record ListBlockItemMetaData
{
    [JsonPropertyName("checked")] public bool Checked { get; set; }
    [JsonPropertyName("start")] public int Start { get; set; } = 1;
    [JsonPropertyName("counterType")] public string CounterType { get; set; } = "numeric";
}
