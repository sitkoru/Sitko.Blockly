using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.EntityFrameworkCore;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.App.Collections;
using Sitko.Core.Blazor.AntDesignComponents;
using Sitko.Core.Repository;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Sitko.Blockly.Tests
{
    public class ChangesTest : BaseTest<BlocklyTestScope>
    {
        public ChangesTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task Changes()
        {
            var scope = await GetScopeAsync();
            var repository = scope.Get<TestRepository>();
            var model = await repository.GetAsync();
            Assert.NotNull(model);
            Assert.NotEmpty(model!.Blocks);
            Assert.False(await repository.HasChangesAsync(model));
            model.Blocks.Add(new TextBlock { Text = "baz", Position = 3 });
            Assert.True(await repository.HasChangesAsync(model));
        }

        [Fact]
        public async Task FormChanges()
        {
            var scope = await GetScopeAsync();
            var repository = scope.Get<TestRepository>();
            var model = await repository.GetAsync();
            Assert.NotNull(model);
            Assert.NotEmpty(model!.Blocks);
            Assert.False(await repository.HasChangesAsync(model));
            var blocks = new OrderedCollection<ContentBlock>();
            blocks.SetItems(model.Blocks);
            model.Blocks = new List<ContentBlock>(blocks.ToList());
            Assert.False(await repository.HasChangesAsync(model));
            blocks.AddItem(new TextBlock { Text = "baz", Position = 3 });
            model.Blocks = new List<ContentBlock>(blocks.ToList());
            Assert.True(await repository.HasChangesAsync(model));
        }

        [Fact]
        public async Task FormPositionChanges()
        {
            var scope = await GetScopeAsync();
            var repository = scope.Get<TestRepository>();
            var model = await repository.GetAsync();
            Assert.NotNull(model);
            Assert.NotEmpty(model!.Blocks);
            Assert.False(await repository.HasChangesAsync(model));
            var blocks = new OrderedCollection<ContentBlock>();
            blocks.SetItems(model.Blocks);
            model.Blocks = new List<ContentBlock>(blocks.ToList());
            Assert.False(await repository.HasChangesAsync(model));
            blocks.MoveUp(model.Blocks.Last());
            model.Blocks = new List<ContentBlock>(blocks.ToList());
            Assert.True(await repository.HasChangesAsync(model));
        }

        [Fact]
        public async Task Form()
        {
            var scope = await GetScopeAsync();
            var repository = scope.Get<TestRepository>();
            var model = await repository.GetAsync();
            Assert.NotNull(model);
            Assert.NotEmpty(model!.Blocks);
            Assert.False(await repository.HasChangesAsync(model));
            var editContext = new EditContext(model);
            var form = scope.Get<TestForm>();
            form.SetEditContext(editContext);
            await form.InitializeAsync(model);
            Assert.True(editContext.Validate());
            var blocks = new OrderedCollection<ContentBlock>();
            blocks.SetItems(form.Blocks);
            blocks.MoveUp(model.Blocks.Last());
            Assert.True(await repository.HasChangesAsync(model));
            Assert.False(form.CanSave());
            await form.FieldChangedAsync(FieldIdentifier.Create(() => model.Blocks));
            Assert.True(editContext.Validate());
        }
    }

    public class BlocklyTestScope : DbBaseTestScope<TestApplication, TestBlocklyDbContext>
    {
        protected override async Task InitDbContextAsync(TestBlocklyDbContext dbContext)
        {
            await base.InitDbContextAsync(dbContext);
            var model = new TestModel();
            model.Blocks.Add(new TextBlock { Text = "Foo", Position = 0 });
            model.Blocks.Add(new CutBlock { ButtonText = "Cut", Position = 1 });
            model.Blocks.Add(new TextBlock { Text = "Bar", Position = 2 });
            await dbContext.AddAsync(model);
            await dbContext.SaveChangesAsync();
        }
    }

    public class TestApplication : AntBlazorApplication<TestStartup>
    {
        public TestApplication(string[] args) : base(args)
        {
            AddModule<EFRepositoriesModule<TestBlocklyDbContext>, EFRepositoriesModuleOptions>();
            AddModule<BlocklyModule, BlocklyModuleOptions>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.AddBlock<TextBlockDescriptor, TextBlock>();
                    moduleConfig.AddBlock<CutBlockDescriptor, CutBlock>();
                });
        }
    }

    public class TestStartup : AntBlazorStartup
    {
        public TestStartup(IConfiguration configuration, IHostEnvironment environment) : base(configuration,
            environment)
        {
        }
    }

    public class TestBlocklyDbContext : DbContext
    {
        public DbSet<TestModel> TestModels => Set<TestModel>();

        public TestBlocklyDbContext(DbContextOptions<TestBlocklyDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterBlocklyConversion<TestModel>(model => model.Blocks, nameof(TestModel.Blocks));
        }
    }

    public class TestModel : IEntity<Guid>
    {
        public object GetId()
        {
            return Id;
        }

        public Guid Id { get; set; } = Guid.NewGuid();

        public List<ContentBlock> Blocks { get; set; } = new();
    }

    public class TestRepository : EFRepository<TestModel, Guid, TestBlocklyDbContext>
    {
        public TestRepository(EFRepositoryContext<TestModel, Guid, TestBlocklyDbContext> repositoryContext) : base(
            repositoryContext)
        {
        }
    }

    public class TestForm : BaseRepositoryForm<TestModel, Guid>
    {
        public TestForm(IRepository<TestModel, Guid> repository, ILogger<TestForm> logger) : base(repository, logger)
        {
        }

        protected override Task MapEntityAsync(TestModel entity)
        {
            entity.Blocks = new List<ContentBlock>(Blocks);
            return Task.CompletedTask;
        }

        protected override Task MapFormAsync(TestModel entity)
        {
            Blocks = new List<ContentBlock>(entity.Blocks);
            return Task.CompletedTask;
        }

        public List<ContentBlock> Blocks { get; set; } = new();
    }
}
