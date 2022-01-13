using Sitko.Blockly.Blocks;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Sitko.Blockly.Tests;

public class TwitterTests : BaseTest
{
    public TwitterTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void Url()
    {
        var url = "https://twitter.com/sonicbw/status/1394170653364953091";
        var block = new TwitterBlock();
        Assert.True(string.IsNullOrEmpty(block.TweetId));
        Assert.True(string.IsNullOrEmpty(block.TweetAuthor));
        block.Url = url;
        Assert.Equal("1394170653364953091", block.TweetId);
        Assert.Equal("sonicbw", block.TweetAuthor);
    }
}
