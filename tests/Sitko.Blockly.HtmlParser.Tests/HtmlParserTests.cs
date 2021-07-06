using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Blockly.Blocks;
using Sitko.Blockly.Tests;
using Sitko.Core.Storage;
using Sitko.Core.Storage.FileSystem;
using Sitko.Core.Xunit;
using Xunit;
using Xunit.Abstractions;
using TestApplication = Sitko.Blockly.Tests.TestApplication;

namespace Sitko.Blockly.HtmlParser.Tests
{
    public class HtmlParserTests : BaseTest<BlocklyHtmlParserTestScope>

    {
        public HtmlParserTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        [Fact]
        public async Task Parse()
        {
            var scope = await GetScopeAsync();
            var storage = scope.Get<IStorage<TestBlocklyStorageOptions>>();
            var filesUploaderLogger = scope.GetLogger<FilesUploader<TestBlocklyStorageOptions>>();
            var httpClientFactory = scope.Get<IHttpClientFactory>();
            var parserLogger = scope.GetLogger<HtmlParser<TestBlocklyStorageOptions>>();
            var filesUploader =
                new FilesUploader<TestBlocklyStorageOptions>(httpClientFactory, storage, filesUploaderLogger,
                    (_, headers, _) =>
                    {
                        var metadata = new {Date = headers.Date.ToString()};
                        return Task.FromResult<object>(metadata);
                    });
            var parser = new HtmlParser<TestBlocklyStorageOptions>(filesUploader, parserLogger);
            var html = await File.ReadAllTextAsync("test.html");
            var blocks = await parser.ParseAsync(html, "test");
            Assert.NotEmpty(blocks);
            Assert.Equal(18, blocks.Count);
            CheckTextBlock(GetBlock(blocks, 0), "Text paragraph");
            CheckTextBlock(GetBlock(blocks, 1), "Plain text");
            CheckTextBlock(GetBlock(blocks, 2), "Div with text");
            CheckTextBlock(GetBlock(blocks, 3), "Div with image");
            CheckGalleryBlock(GetBlock(blocks, 4), new[] {"googlelogo_light_color_272x92dp.png"});
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
            CheckGalleryBlock(GetBlock(blocks, 15), new[] {"googlelogo_light_color_272x92dp.png"});
            CheckQuoteBlock(GetBlock(blocks, 16), "Quote text");
            CheckTwitchBlock(GetBlock(blocks, 17), "ourchickenlife");
        }

        private void CheckTwitchBlock(ContentBlock block, string expectedChannel)
        {
            CheckBlock<TwitchBlock>(block, twitchBlock => Assert.Equal(expectedChannel, twitchBlock.ChannelId));
        }

        private ContentBlock GetBlock(List<ContentBlock> blocks, int position)
        {
            Assert.True(blocks.Count > position);
            var block = blocks[position];
            Assert.Equal(position, block.Position);
            return block;
        }

        private void CheckBlock<TContentBlock>(ContentBlock block, Action<TContentBlock> check)
            where TContentBlock : ContentBlock
        {
            Assert.IsType<TContentBlock>(block);
            if (block is TContentBlock typedBlock)
            {
                check(typedBlock);
            }
        }

        private void CheckYoutubeBlock(ContentBlock block, string expectedYoutubeId)
        {
            CheckBlock<YoutubeBlock>(block, youtubeBlock => Assert.Equal(expectedYoutubeId, youtubeBlock.YoutubeId));
        }

        private void CheckIframeBlock(ContentBlock block, string expectedSrc)
        {
            CheckBlock<IframeBlock>(block, iframeBlock => Assert.Equal(expectedSrc, iframeBlock.Src));
        }

        private void CheckTextBlock(ContentBlock block, string expectedText)
        {
            CheckBlock<TextBlock>(block, textBlock => Assert.Equal(expectedText, textBlock.Text));
        }

        private void CheckQuoteBlock(ContentBlock block, string expectedText)
        {
            CheckBlock<QuoteBlock>(block, quoteBlock => Assert.Equal(expectedText, quoteBlock.Text));
        }

        private void CheckGalleryBlock(ContentBlock block, string[] fileNames)
        {
            CheckBlock<GalleryBlock>(block, galleryBlock =>
            {
                Assert.Equal(fileNames.Length, galleryBlock.Pictures.Count);
                Assert.All(fileNames,
                    fileName => Assert.Single(galleryBlock.Pictures.Where(p => p.FileName == fileName)));
            });
        }
    }

    public class BlocklyHtmlParserTestScope : BlocklyTestScope
    {
        protected override TestApplication ConfigureApplication(TestApplication application, string name)
        {
            base.ConfigureApplication(application, name);
            application.AddModule<FileSystemStorageModule<TestBlocklyStorageOptions>, TestBlocklyStorageOptions>(
                (_, _, moduleConfig) =>
                {
                    moduleConfig.StoragePath = Path.Combine(Path.GetTempPath(), nameof(BlocklyHtmlParserTestScope));
                });
            application
                .AddModule<FileSystemStorageMetadataModule<TestBlocklyStorageOptions>,
                    FileSystemStorageMetadataProviderOptions>();
            return application;
        }

        protected override IServiceCollection ConfigureServices(IConfiguration configuration,
            IHostEnvironment environment,
            IServiceCollection services, string name)
        {
            services.AddHttpClient();
            return base.ConfigureServices(configuration, environment, services, name);
        }
    }

    public class TestBlocklyStorageOptions : StorageOptions, IFileSystemStorageOptions
    {
        public override string Name { get; set; } = "Blockly";
        public string StoragePath { get; set; } = "";
    }
}
