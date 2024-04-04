using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Json;

public class ContentBlockConverter : JsonConverter<ContentBlock>
{
    private readonly JsonSerializerOptions jsonSerializerOptions;
    private const string TypePropertyName = "type";

    public ContentBlockConverter()
    {
        var polymorphismOptions = new JsonPolymorphismOptions()
        {
            TypeDiscriminatorPropertyName = TypePropertyName,
            IgnoreUnrecognizedTypeDiscriminators = true,
            UnknownDerivedTypeHandling = JsonUnknownDerivedTypeHandling.FailSerialization
        };

        foreach (var (key, type) in ContentBlocksRegistry.GetBlockTypes())
        {
            polymorphismOptions.DerivedTypes.Add(new JsonDerivedType(type, key));
        }

        jsonSerializerOptions = new JsonSerializerOptions()
        {
            TypeInfoResolver = new ContentBlocksTypeResolver(polymorphismOptions)
        };
    }

    public override ContentBlock? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var readerClone = reader;
        var typePropertyFound = false;
        while (readerClone.Read())
        {
            if (readerClone.TokenType != JsonTokenType.PropertyName)
            {
                continue;
            }

            var propertyName = readerClone.GetString();
            if (propertyName != TypePropertyName)
            {
                continue;
            }

            typePropertyFound = true;

            break;
        }

        ContentBlock? block = null;
        if (typePropertyFound)
        {
            readerClone.Read();
            if (readerClone.TokenType != JsonTokenType.String)
            {
                throw new JsonException();
            }

            var key = readerClone.GetString();

            if (!string.IsNullOrEmpty(key))
            {
                var descriptor = ContentBlocksRegistry.GetBlockMetadata(key);
                if (JsonSerializer.Deserialize(ref reader, descriptor.BlockType) is ContentBlock contentBlock)
                {
                    block = contentBlock;
                }
            }
        }

        return block;
    }

    public override void Write(Utf8JsonWriter writer, ContentBlock value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, value, typeof(IContentBlock), jsonSerializerOptions);
}