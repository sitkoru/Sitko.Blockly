using System;
using System.Linq;

namespace Sitko.Blockly.Blocks
{
    public record YoutubeBlock : UrlContentBlock
    {
        protected override bool IsEmpty => string.IsNullOrEmpty(YoutubeId);
        protected override string FinalUrl => $"https://www.youtube.com/embed/{YoutubeId}";

        protected override void ParseUrl(string? url)
        {
            if (!string.IsNullOrEmpty(url) && (url.Contains("http://") || url.Contains("https://")))
            {
                var uri = new Uri(url);

                var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

                YoutubeId = queryParams.ContainsKey("v") ? queryParams["v"][0] : uri.Segments.Last();
            }
            else
            {
                YoutubeId = string.Empty;
            }
        }

        public override string ToString()
        {
            return $"Youtube: {YoutubeId}";
        }

        public string YoutubeId { get; set; } = "";
    }
}
