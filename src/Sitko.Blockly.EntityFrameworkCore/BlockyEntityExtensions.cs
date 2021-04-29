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
            modelBuilder.RegisterJsonCollectionConversion<TEntity, ContentBlock>(
                entity => entity.Blocks,
                fieldName);

            return modelBuilder;
        }
    }
}
