using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Sitko.Core.Db.Postgres;

namespace Sitko.Blockly.EntityFrameworkCore
{
    public static class BlocklyEntityExtensions
    {
        public static ModelBuilder RegisterBlocklyConversion<TEntity>(this ModelBuilder modelBuilder,
            Expression<Func<TEntity, List<ContentBlock>>> fieldSelector,
            string fieldName, bool isRequired = true)
            where TEntity : class, new()
        {
            modelBuilder.RegisterJsonEnumerableConversion<TEntity, ContentBlock, List<ContentBlock>>(
                fieldSelector,
                fieldName, false);
            if (isRequired)
            {
                modelBuilder.Entity<TEntity>().Property(fieldSelector).IsRequired().HasDefaultValueSql("'[]'");
            }

            return modelBuilder;
        }
    }
}
