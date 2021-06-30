using System;
using System.Linq;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks
{
    public record TwitterBlock : UrlContentBlock
    {
        protected override bool IsEmpty => string.IsNullOrEmpty(TweetId);
        protected override string FinalUrl => $"https://twitter.com/{TweetAuthor}/status/{TweetId}";

        public string TweetId { get; set; } = "";
        public string TweetAuthor { get; set; } = "";

        protected override void ParseUrl(string? url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var uri = new Uri(url);

                if (uri.Segments.Length == 4)
                {
                    var author = uri.Segments[1].Replace("/", "");
                    var tweetId = uri.Segments.Last();
                    if (TweetAuthor != author || TweetId != tweetId)
                    {
                        TweetAuthor = author;
                        TweetId = tweetId;
                    }
                }
            }
            else
            {
                TweetId = string.Empty;
                TweetAuthor = string.Empty;
            }
        }

        public override string ToString()
        {
            return $"Twitter: {TweetId} by {TweetAuthor}";
        }
    }

    public record TwitterBlockDescriptor : BlockDescriptor<TwitterBlock>
    {
        public override int Priority => 7;

        public TwitterBlockDescriptor(ILocalizationProvider<TwitterBlock> localizationProvider) : base(
            localizationProvider)
        {
        }
    }
}
