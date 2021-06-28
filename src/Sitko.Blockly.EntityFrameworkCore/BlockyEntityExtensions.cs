using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Sitko.Core.Db.Postgres;

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
                fieldName, false);

            return modelBuilder;
        }
    }
}
