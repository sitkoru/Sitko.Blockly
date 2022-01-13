using Sitko.Blockly.Blocks;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Sitko.Blockly.Tests;

public class TwitchTests : BaseTest
{
    public TwitchTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public void Video()
    {
        var url = "https://www.twitch.tv/videos/1064724482";
        var block = new TwitchBlock();
        Assert.True(string.IsNullOrEmpty(block.VideoId));
        block.Url = url;
        Assert.Equal("1064724482", block.VideoId);
        Assert.Equal(url, block.Url);
    }

    [Fact]
    public void EmbedVideo()
    {
        var url = "https://player.twitch.tv/?video=1064724482&parent=www.example.com";
        var block = new TwitchBlock();
        Assert.True(string.IsNullOrEmpty(block.VideoId));
        block.Url = url;
        Assert.Equal("1064724482", block.VideoId);
        Assert.Equal("https://www.twitch.tv/videos/1064724482", block.Url);
    }

    [Fact]
    public void Channel()
    {
        var url = "https://www.twitch.tv/mrllamasc";
        var block = new TwitchBlock();
        Assert.True(string.IsNullOrEmpty(block.ChannelId));
        block.Url = url;
        Assert.Equal("mrllamasc", block.ChannelId);
        Assert.Equal(url, block.Url);
    }

    [Fact]
    public void EmbedChannel()
    {
        var url = "https://player.twitch.tv/?channel=mrllamasc&parent=www.example.com";
        var block = new TwitchBlock();
        Assert.True(string.IsNullOrEmpty(block.ChannelId));
        block.Url = url;
        Assert.Equal("mrllamasc", block.ChannelId);
        Assert.Equal("https://www.twitch.tv/mrllamasc", block.Url);
    }

    [Fact]
    public void Collection()
    {
        var url = "https://www.twitch.tv/collections/yc6UIzOXRhb93g?filter=collections";
        var block = new TwitchBlock();
        Assert.True(string.IsNullOrEmpty(block.CollectionId));
        block.Url = url;
        Assert.Equal("yc6UIzOXRhb93g", block.CollectionId);
        Assert.Equal(url, block.Url);
    }

    [Fact]
    public void EmbedCollection()
    {
        var url = "https://player.twitch.tv/?collection=yc6UIzOXRhb93g&video=803727802&parent=www.example.com";
        var block = new TwitchBlock();
        Assert.True(string.IsNullOrEmpty(block.CollectionId));
        block.Url = url;
        Assert.Equal("yc6UIzOXRhb93g", block.CollectionId);
        Assert.Equal("803727802", block.VideoId);
        Assert.Equal("https://www.twitch.tv/videos/803727802?collection=yc6UIzOXRhb93g&filter=collections", block.Url);
    }
}
