using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Sitko.Blockly.EntityFrameworkCore
{
    public static class BlocklyEntityExtensions
    {
        public static ModelBuilder RegisterBlocklyConversion<TEntity>(this ModelBuilder modelBuilder,
            string fieldName = "Blocks")
            where TEntity : class, IBlocklyEntity
        {
            modelBuilder.RegisterJsonEnumerableConversion<TEntity, ContentBlock, List<ContentBlock>>(
                entity => entity.Blocks,
                fieldName);

            return modelBuilder;
        }
        
        // TODO: DROP AFTER NEW SITKO.CORE RELEASE
        public static void RegisterJsonEnumerableConversion<TEntity, TData, TEnumerable>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, TEnumerable>> getProperty, string name)
            where TEntity : class
            where TEnumerable : IEnumerable<TData>, new()
        {
            var valueComparer = new ValueComparer<TEnumerable>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => Deserialize<TEnumerable>(Serialize(c)!)!);
            modelBuilder
                .Entity<TEntity>()
                .Property(getProperty)
                .HasColumnType("jsonb")
                .HasColumnName(name)
                .HasConversion(data => Serialize(data),
                    json => Deserialize<TEnumerable>(json) ?? new TEnumerable())
                .Metadata.SetValueComparer(valueComparer);
        }

        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto,
            MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead
        };

        private static string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, _jsonSettings);
        }

        private static T? Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
        }
    }
}
