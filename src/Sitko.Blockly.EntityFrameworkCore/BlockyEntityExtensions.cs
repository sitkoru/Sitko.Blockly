using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Sitko.Blockly.Json;

namespace Sitko.Blockly.EntityFrameworkCore
{
    using JetBrains.Annotations;

    public static class BlocklyEntityExtensions
    {
        [PublicAPI]
        public static ModelBuilder RegisterBlocklyConversion<TEntity>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, List<ContentBlock>>> fieldSelector,
            string fieldName, bool isRequired = true)
            where TEntity : class =>
            modelBuilder.RegisterBlocklyConversion(fieldSelector, fieldName,
                isRequired, new List<ContentBlock>());

        [PublicAPI]
        public static ModelBuilder RegisterBlocklyConversion<TEntity, TEnumerable>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, TEnumerable>> fieldSelector,
            string fieldName, bool isRequired = true, TEnumerable? defaultValue = default)
            where TEntity : class
            where TEnumerable : IEnumerable<ContentBlock>, new()
        {
            var valueComparer = new ValueComparer<TEnumerable>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v!.GetHashCode())),
                c => BlocklyJsonExtensions.DeserializeBlocks<TEnumerable>(BlocklyJsonExtensions.SerializeBlocks(c))!);
            modelBuilder
                .Entity<TEntity>()
                .Property(fieldSelector)
                .HasColumnType("jsonb")
                .HasColumnName(fieldName)
                .HasConversion(data => BlocklyJsonExtensions.SerializeBlocks(data),
                    json => BlocklyJsonExtensions.DeserializeBlocks<TEnumerable>(json) ?? defaultValue!)
                .Metadata.SetValueComparer(valueComparer);
            if (isRequired)
            {
                modelBuilder.Entity<TEntity>().Property(fieldSelector).IsRequired().HasDefaultValueSql("'[]'");
            }

            return modelBuilder;
        }
    }
}
