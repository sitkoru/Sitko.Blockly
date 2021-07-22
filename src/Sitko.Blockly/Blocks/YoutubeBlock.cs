using System;
using System.Linq;
using Sitko.Core.App.Localization;

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

        public override string ToString() => $"Youtube: {YoutubeId}";

        public string YoutubeId { get; set; } = "";
    }

    public record YoutubeBlockDescriptor : BlockDescriptor<YoutubeBlock>
    {
        public override int Priority => 6;

        public YoutubeBlockDescriptor(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
            localizationProvider)
        {
        }
    }
}
