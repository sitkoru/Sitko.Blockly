using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Collections;
using Sitko.Core.Storage;
using System.Globalization;

namespace Sitko.Blockly.HtmlParser
{
    using System;

    public class HtmlParser<TStorageOptions> where TStorageOptions : StorageOptions
    {
        private readonly ILogger<HtmlParser<TStorageOptions>> logger;

        private readonly FilesUploader<TStorageOptions> filesUploader;

        public HtmlParser(FilesUploader<TStorageOptions> filesUploader, ILogger<HtmlParser<TStorageOptions>> logger)
        {
            this.logger = logger;
            this.filesUploader = filesUploader;
        }

        protected virtual Task<ContentBlock> ParseIframeAsync(HtmlNode node)
        {
            var srcUrl = node.Attributes["src"].Value;
            ContentBlock block;
            if (srcUrl.Contains("youtube.com"))
            {
                block = new YoutubeBlock { Url = srcUrl };
            }
            else if (srcUrl.Contains("player.twitch.tv"))
            {
                block = new TwitchBlock { Url = srcUrl };
            }
            else
            {
                block = new IframeBlock { Src = srcUrl };
            }

            return Task.FromResult(block);
        }

        protected virtual async Task<ContentBlock?> ParseImgAsync(HtmlNode node, string uploadPath)
        {
            var imgUrl = node.Attributes["src"].Value;
            var item = await filesUploader.UploadFromUrlAsync(imgUrl, uploadPath);
            if (item != null)
            {
                var pictures = new ValueCollection<StorageItem> { item };
                return new GalleryBlock { Pictures = pictures };
            }

            return null;
        }

        public async Task<List<ContentBlock>> ParseAsync(string html, string uploadPath)
        {
            html = html.Replace("&amp;ndash;", "&ndash;")
                .Replace("&amp;nbsp;", "&nbsp;")
                .Replace("&amp;mdash;", "&mdash;")
                .Replace("&amp;quote;", "&quote;")
                .Replace("&amp;laquo;", "&laquo;")
                .Replace("&amp;raquo;", "&raquo;")
                .Replace("'", "''")
                .Replace("\r", "")
                .Replace("\n", "");

            var blocks = new OrderedCollection<ContentBlock>();
            var document = new HtmlDocument();
            document.LoadHtml(html);
            var xpath = "//text()[not(normalize-space())]";
            var emptyNodes = document.DocumentNode.SelectNodes(xpath);
            emptyNodes?.ToList().ForEach(emptyNode => emptyNode.Remove());

            var nodes = new List<HtmlNode>();
            string? currentTextNode = null;
            foreach (var childNode in document.DocumentNode.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "a":
                    case "span":
                    case "strong":
                    case "h1":
                    case "h2":
                    case "h3":
                    case "h4":
                    case "h5":
                    case "h6":
                    case "em":
                    case "b":
                    case "i":
                    case "center":
                    case "small":
                    case "del":
                    case "dt":
                    case "s":
                    case "noindex":
                    case "style":
                    case "font":
                    case "nobr":
                    case "script":
                    case "#text":
                    case "noscript":
                    case "pre":
                    case "aside":
                    case "article":
                    case "u":
                        if (currentTextNode == null)
                        {
                            currentTextNode = childNode.OuterHtml;
                        }
                        else
                        {
                            currentTextNode += childNode.OuterHtml;
                        }

                        break;
                    case "br":
                        if (currentTextNode != null)
                        {
                            currentTextNode += childNode.OuterHtml.Trim();
                        }

                        break;
                    default:
                        if (!string.IsNullOrEmpty(currentTextNode))
                        {
                            nodes.Add(HtmlNode.CreateNode($"<div>{currentTextNode.Trim()}</div>"));
                            currentTextNode = null;
                        }

                        nodes.Add(childNode);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(currentTextNode))
            {
                nodes.Add(HtmlNode.CreateNode($"<div>{currentTextNode.Trim()}</div>"));
            }

            foreach (var childNode in nodes)
            {
                var nodeBlocks = await ParseNodeAsync(childNode, uploadPath);
                if (nodeBlocks.Length > 0)
                {
                    blocks.AddItems(nodeBlocks);
                }
            }

            return blocks.ToList();
        }

        protected async Task<ContentBlock[]> ParseAsync(HtmlNode node, string uploadPath)
        {
            var blocks = new List<ContentBlock>();
            var extractedBlocks = new List<ContentBlock>();
            foreach (var childNode in node.ChildNodes.ToArray())
            {
                var nodeBlocks = await ParseNodeAsync(childNode, uploadPath);
                if (nodeBlocks.Length > 0)
                {
                    extractedBlocks.AddRange(nodeBlocks);
                    var nodeNewHtml =
                        $"<block id=\"{extractedBlocks.Count.ToString(CultureInfo.InvariantCulture)}\" />";
                    var newNode = HtmlNode.CreateNode(nodeNewHtml);
                    childNode.ParentNode.ReplaceChild(newNode, childNode);
                }
            }

            var currentHtml = "";
            foreach (var childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "block":
                        if (!string.IsNullOrEmpty(currentHtml))
                        {
                            blocks.Add(new TextBlock { Text = currentHtml.Trim() });
                            currentHtml = "";
                        }

                        var id = int.Parse(childNode.Attributes["id"].Value, CultureInfo.InvariantCulture);
                        blocks.Add(extractedBlocks[id - 1]);
                        break;
                    default:
                        if (!string.IsNullOrEmpty(childNode.InnerText.Replace("&nbsp;", "").Trim()))
                        {
                            currentHtml += childNode.OuterHtml;
                        }

                        break;
                }
            }

            if (!string.IsNullOrEmpty(currentHtml))
            {
                blocks.Add(new TextBlock { Position = blocks.Count, Text = currentHtml.Trim() });
            }

            return blocks.ToArray();
        }

        protected async Task<ContentBlock[]> ParseNodeAsync(HtmlNode childNode, string uploadPath)
        {
            var blocks = new List<ContentBlock>();
            switch (childNode.Name)
            {
                case "p":
                case "#text":
                    blocks.AddRange(await ParseAsync(childNode, uploadPath));
                    break;
                case "div":
                    blocks.AddRange(await ParseAsync(childNode, uploadPath));
                    break;
                case "blockquote":
                    blocks.Add(await ParseBlockQuoteAsync(childNode));
                    break;
                case "iframe":
                    blocks.Add(await ParseIframeAsync(childNode));
                    break;
                case "ul":
                case "ol":
                case "table":
                case "hr":
                    blocks.Add(await ParseHtmlAsync(childNode));
                    break;
                case "img":
                    var imgBlock = await ParseImgAsync(childNode, uploadPath);
                    if (imgBlock != null)
                    {
                        blocks.Add(imgBlock);
                    }

                    break;
                default:
                    var (parsed, parsedBlocks) = await TryParseAsync(childNode, uploadPath);
                    if (parsed)
                    {
                        blocks.AddRange(parsedBlocks);
                    }
                    else
                    {
                        logger.LogWarning("Unknown node type: {NodeType}", childNode.Name);
                    }

                    break;
            }

            return blocks.ToArray();
        }

        protected virtual Task<(bool parsed, ContentBlock[] contentBlocks)> TryParseAsync(HtmlNode childNode,
            string uploadPath) =>
            Task.FromResult((false, Array.Empty<ContentBlock>()));

        private static Task<ContentBlock> ParseHtmlAsync(HtmlNode childNode)
        {
            ContentBlock block = new TextBlock { Text = childNode.OuterHtml.Trim() };
            return Task.FromResult(block);
        }

        protected virtual Task<ContentBlock> ParseBlockQuoteAsync(HtmlNode childNode)
        {
            var block = new QuoteBlock { Text = childNode.InnerHtml.Trim() };
            return Task.FromResult<ContentBlock>(block);
        }
    }
}
