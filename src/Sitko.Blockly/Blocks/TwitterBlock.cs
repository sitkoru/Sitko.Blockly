using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sitko.Blockly.Blocks
{
    public record TwitterBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"Twitter: {TweetId} by {TweetAuthor}";
        }

        public string TweetId { get; set; } = "";
        public string TweetAuthor { get; set; } = "";

        [JsonIgnore]
        public string? TweetLink
        {
            get
            {
                return string.IsNullOrEmpty(TweetId) ? null : $"https://twitter.com/{TweetAuthor}/status/{TweetId}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var uri = new Uri(value);

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
        }
    }
}
