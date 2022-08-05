using HtmlAgilityPack;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.Tests;
using Sitko.Core.App;
using Sitko.Core.Storage;
using Sitko.Core.Storage.FileSystem;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;

namespace Sitko.Blockly.HtmlParser.Tests;

public class HtmlParserTests : BaseTest<BlocklyHtmlParserTestScope>

{
    public HtmlParserTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
    }

    [Fact]
    public async Task Parse()
    {
        var scope = await GetScopeAsync();
        var storage = scope.GetService<IStorage<TestBlocklyStorageOptions>>();
        var filesUploaderLogger = scope.GetLogger<FilesUploader<TestBlocklyStorageOptions>>();
        var httpClientFactory = scope.GetService<IHttpClientFactory>();
        var parserLogger = scope.GetLogger<TestHtmlParser<TestBlocklyStorageOptions>>();
        var filesUploader =
            new FilesUploader<TestBlocklyStorageOptions>(httpClientFactory, storage, filesUploaderLogger,
                (_, headers, _) =>
                {
                    var metadata = new { Date = headers.Date.ToString() };
                    return Task.FromResult<object>(metadata);
                });
        var parser = new TestHtmlParser<TestBlocklyStorageOptions>(filesUploader, parserLogger);
        var html = await File.ReadAllTextAsync("test.html");
        var blocks = await parser.ParseAsync(html, "test");
        Assert.NotEmpty(blocks);
        //Assert.Equal(19, blocks.Count);
        CheckTextBlock(GetBlock(blocks, 0), "Text paragraph");
        CheckTextBlock(GetBlock(blocks, 1), "Plain text");
        CheckTextBlock(GetBlock(blocks, 2), "Div with text");
        CheckTextBlock(GetBlock(blocks, 3), "Div with image");
        CheckGalleryBlock(GetBlock(blocks, 4), new[] { "googlelogo_light_color_272x92dp.png" });
        CheckTextBlock(GetBlock(blocks, 5), "Div with iframe");
        CheckIframeBlock(GetBlock(blocks, 6), "https://en.wikipedia.org");
        CheckTextBlock(GetBlock(blocks, 7), "Div with YouTube");
        CheckYoutubeBlock(GetBlock(blocks, 8), "_-NoFmhqNu4");
        CheckIframeBlock(GetBlock(blocks, 9), "https://en.wikipedia.org");
        CheckYoutubeBlock(GetBlock(blocks, 10), "_-NoFmhqNu4");
        CheckTextBlock(GetBlock(blocks, 11), "<ul><li>Ul item</li></ul>");
        CheckTextBlock(GetBlock(blocks, 12), "<ol><li>Ol item</li></ol>");
        CheckTextBlock(GetBlock(blocks, 13), "<table><tr><td>Table</td><td>Column</td></tr></table>");
        CheckTextBlock(GetBlock(blocks, 14), "<hr/>");
        CheckGalleryBlock(GetBlock(blocks, 15), new[] { "googlelogo_light_color_272x92dp.png" });
        CheckQuoteBlock(GetBlock(blocks, 16), "Quote text");
        CheckTwitchBlock(GetBlock(blocks, 17), "ourchickenlife");
    }

    private static void CheckTwitchBlock(ContentBlock block, string expectedChannel) => CheckBlock<TwitchBlock>(
        block,
        twitchBlock => Assert.Equal(expectedChannel, twitchBlock.ChannelId));

    private static ContentBlock GetBlock(List<ContentBlock> blocks, int position)
    {
        Assert.True(blocks.Count > position);
        var block = blocks[position];
        Assert.Equal(position, block.Position);
        return block;
    }

    private static void CheckBlock<TContentBlock>(ContentBlock block, Action<TContentBlock> check)
        where TContentBlock : ContentBlock
    {
        Assert.IsType<TContentBlock>(block);
        if (block is TContentBlock typedBlock)
        {
            check(typedBlock);
        }
    }

    private static void CheckYoutubeBlock(ContentBlock block, string expectedYoutubeId) => CheckBlock<YoutubeBlock>(
        block,
        youtubeBlock => Assert.Equal(expectedYoutubeId, youtubeBlock.YoutubeId));

    private static void CheckIframeBlock(ContentBlock block, string expectedSrc) =>
        CheckBlock<IframeBlock>(block, iframeBlock => Assert.Equal(expectedSrc, iframeBlock.Src));

    private static void CheckTextBlock(ContentBlock block, string expectedText) =>
        CheckBlock<TextBlock>(block, textBlock => Assert.Equal(expectedText, textBlock.Text));

    private static void CheckQuoteBlock(ContentBlock block, string expectedText) =>
        CheckBlock<QuoteBlock>(block, quoteBlock => Assert.Equal(expectedText, quoteBlock.Text));

    private static void CheckGalleryBlock(ContentBlock block, string[] fileNames) =>
        CheckBlock<GalleryBlock>(block, galleryBlock =>
        {
            Assert.Equal(fileNames.Length, galleryBlock.Pictures.Count);
            Assert.All(fileNames,
                fileName => Assert.Single(galleryBlock.Pictures.Where(p => p.FileName == fileName)));
        });
}

public class BlocklyHtmlParserTestScope : BlocklyTestScope
{
    protected override TestApplication ConfigureApplication(TestApplication application, string name)
    {
        base.ConfigureApplication(application, name);
        application
            .AddFileSystemStorage<TestBlocklyStorageOptions>(moduleOptions =>
            {
                moduleOptions.StoragePath = Path.Combine(Path.GetTempPath(), nameof(BlocklyHtmlParserTestScope));
            })
            .AddFileSystemStorageMetadata<TestBlocklyStorageOptions>();
        return application;
    }

    protected override IServiceCollection ConfigureServices(IApplicationContext applicationContext,
        IServiceCollection services, string name)
    {
        services.AddHttpClient();
        return base.ConfigureServices(applicationContext, services, name);
    }
}

public class TestBlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
{
    public string StoragePath { get; set; } = "";
}

public class TestHtmlParser<TStorageOptions> : HtmlParser<TStorageOptions> where TStorageOptions : StorageOptions
{
    public TestHtmlParser(FilesUploader<TStorageOptions> filesUploader,
        ILogger<TestHtmlParser<TStorageOptions>> logger) : base(filesUploader, logger)
    {
    }

    protected override async Task<(bool parsed, ContentBlock[] contentBlocks)> TryParseAsync(HtmlNode childNode,
        string uploadPath)
    {
        switch (childNode.Name)
        {
            case "figure":
                var contentBlocks = await ParseAsync(childNode, uploadPath);
                return (true, contentBlocks.ToArray());
            case "figcaption":
                return (true, new ContentBlock[] { new TextBlock { Text = $"<small>{childNode.InnerText}</small>" } });
        }

        return await base.TryParseAsync(childNode, uploadPath);
    }
}
