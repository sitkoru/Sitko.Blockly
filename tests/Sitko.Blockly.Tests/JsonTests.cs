using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.Json;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Sitko.Blockly.Tests
{
    public class JsonTests : BaseTest<BlocklyTestScope>
    {
        public JsonTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task Serialize()
        {
            await GetScopeAsync();
            var blocks = new List<ContentBlock> {new TextBlock {Text = "Foo"}};
            var json = BlocklyJsonExtensions.SerializeBlocks(blocks);
            var deserialized = BlocklyJsonExtensions.DeserializeBlocks(json);
            Assert.NotNull(deserialized);
            Assert.NotEmpty(deserialized!);
            var first = deserialized!.First();
            Assert.IsType<TextBlock>(first);
            Assert.Equal("Foo", ((TextBlock)first).Text);
        }
    }
}
