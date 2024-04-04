using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Json;

internal class ContentBlocksTypeResolver(JsonPolymorphismOptions polymorphismOptions) : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var jsonTypeInfo = base.GetTypeInfo(type, options);

        if (jsonTypeInfo.Type == typeof(IContentBlock))
        {
            jsonTypeInfo.PolymorphismOptions = polymorphismOptions;
        }

        return jsonTypeInfo;
    }
}
