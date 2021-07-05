using System.Collections.Generic;
using System.Text.Json;

namespace Sitko.Blockly.Json
{
    public static class BlocklyJsonExtensions
    {
        public static readonly JsonSerializerOptions BlocklyJsonOptions = new()
        {
            Converters = {new ContentBlockJsonConverter()}
        };

        public static List<ContentBlock>? DeserializeBlocks(string json)
        {
            return DeserializeBlocks<List<ContentBlock>>(json);
        }

        public static TEnumerable? DeserializeBlocks<TEnumerable>(string json)
            where TEnumerable : IEnumerable<ContentBlock>
        {
            return JsonSerializer.Deserialize<TEnumerable>(json, BlocklyJsonOptions);
        }

        public static string SerializeBlocks(IEnumerable<ContentBlock> blocks)
        {
            return JsonSerializer.Serialize(blocks, BlocklyJsonOptions);
        }
    }
}
