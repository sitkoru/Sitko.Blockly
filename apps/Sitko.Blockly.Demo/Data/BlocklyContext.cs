using Microsoft.EntityFrameworkCore;
using Sitko.Blockly.Demo.Data.Entities;
using Sitko.Blockly.EntityFrameworkCore;

namespace Sitko.Blockly.Demo.Data
{
    public class BlocklyContext : DbContext
    {
        public DbSet<Post> Posts => Set<Post>();

        public BlocklyContext(DbContextOptions<BlocklyContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterBlocklyConversion<Post>();
        }
    }
}
