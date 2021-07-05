using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Sitko.Blockly.Json
{
    public class ContentBlockJsonConverter : JsonConverter<IEnumerable<ContentBlock>>
    {
        public const string BlocklyKey = "BlocklyKey";

        public override bool CanConvert(Type typeToConvert) =>
            typeof(IEnumerable<ContentBlock>).IsAssignableFrom(typeToConvert);

        public override IEnumerable<ContentBlock> Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var blocks = new List<ContentBlock>();

            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                Utf8JsonReader readerClone = reader;

                if (readerClone.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                while (readerClone.Read())
                {
                    if (readerClone.TokenType != JsonTokenType.PropertyName)
                    {
                        continue;
                    }

                    var propertyName = readerClone.GetString();
                    if (propertyName != BlocklyKey)
                    {
                        continue;
                    }

                    break;
                }

                readerClone.Read();
                if (readerClone.TokenType != JsonTokenType.String)
                {
                    throw new JsonException();
                }

                var key = readerClone.GetString();
                ContentBlock? block = null;
                if (!string.IsNullOrEmpty(key))
                {
                    var descriptor = Blockly.GetDescriptor(key);
                    if (descriptor is not null)
                    {
                        if (JsonSerializer.Deserialize(ref reader, descriptor.Type) is ContentBlock contentBlock)
                        {
                            block = contentBlock;
                        }
                    }
                }

                if (block is not null)
                {
                    blocks.Add(block);
                }
                else
                {
                    while (reader.TokenType != JsonTokenType.EndObject)
                    {
                        reader.Read();
                    }
                }
            }

            return blocks;
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<ContentBlock> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var block in value)
            {
                var descriptor = Blockly.GetDescriptor(block.GetType());
                if (descriptor is not null)
                {
                    writer.WriteStartObject();
                    writer.WriteString(BlocklyKey, descriptor.Key);
                    foreach (var property in block.GetType().GetProperties())
                    {
                        var val = property.GetValue(block);
                        writer.WritePropertyName(property.Name);
                        JsonSerializer.Serialize(writer, val, property.PropertyType, options);
                    }

                    writer.WriteEndObject();
                }
            }

            writer.WriteEndArray();
        }
    }
}
