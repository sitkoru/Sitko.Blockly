using Sitko.Blockly.Blocks;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Sitko.Blockly.Tests;

public class YoutubeTest : BaseTest
{
    public YoutubeTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void WatchUrl()
    {
        var url = "https://www.youtube.com/watch?v=g6C3zDqXe-Q";
        var block = new YoutubeBlock();
        Assert.True(string.IsNullOrEmpty(block.YoutubeId));
        block.Url = url;
        Assert.Equal("g6C3zDqXe-Q", block.YoutubeId);
    }

    [Fact]
    public void ShortUrl()
    {
        var url = "https://youtu.be/g6C3zDqXe-Q";
        var block = new YoutubeBlock();
        Assert.True(string.IsNullOrEmpty(block.YoutubeId));
        block.Url = url;
        Assert.Equal("g6C3zDqXe-Q", block.YoutubeId);
    }

    [Fact]
    public void EmbedUrl()
    {
        var url = "https://www.youtube.com/embed/g6C3zDqXe-Q";
        var block = new YoutubeBlock();
        Assert.True(string.IsNullOrEmpty(block.YoutubeId));
        block.Url = url;
        Assert.Equal("g6C3zDqXe-Q", block.YoutubeId);
    }
}
