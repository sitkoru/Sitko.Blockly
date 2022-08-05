using System.Text.Json;
using JetBrains.Annotations;

namespace Sitko.Blockly.Json;

public static class BlocklyJsonExtensions
{
    [PublicAPI] public static readonly JsonSerializerOptions BlocklyJsonOptions =
        new() { Converters = { new ContentBlockJsonConverter() } };

    public static List<ContentBlock>? DeserializeBlocks(string json) => DeserializeBlocks<List<ContentBlock>>(json);

    public static TEnumerable? DeserializeBlocks<TEnumerable>(string json)
        where TEnumerable : IEnumerable<ContentBlock> =>
        JsonSerializer.Deserialize<TEnumerable>(json, BlocklyJsonOptions);

    public static string SerializeBlocks(IEnumerable<ContentBlock> blocks) =>
        JsonSerializer.Serialize(blocks, BlocklyJsonOptions);
}
