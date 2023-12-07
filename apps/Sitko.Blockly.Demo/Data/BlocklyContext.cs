using Microsoft.EntityFrameworkCore;
using Sitko.Blockly.Data.Entities;
using Sitko.Blockly.EntityFrameworkCore;

namespace Sitko.Blockly.Demo.Data;

public class BlocklyContext : DbContext
{
    public DbSet<Post> Posts => Set<Post>();

    public BlocklyContext(DbContextOptions<BlocklyContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.RegisterBlocklyConversion<Post>(post => post.Blocks, nameof(Post.Blocks));
        modelBuilder.RegisterBlocklyConversion<Post>(post => post.SecondaryBlocks, nameof(Post.SecondaryBlocks));
    }
}
