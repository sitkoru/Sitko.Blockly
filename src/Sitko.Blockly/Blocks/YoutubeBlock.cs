using System.Web;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks;

[ContentBlockMetadata(6)]
public record YoutubeBlock : UrlContentBlock
{
    protected override bool IsEmpty => string.IsNullOrEmpty(YoutubeId);
    protected override string FinalUrl => $"https://www.youtube.com/embed/{YoutubeId}";

    public string YoutubeId { get; set; } = "";

    protected override void ParseUrl(string? url)
    {
        if (!string.IsNullOrEmpty(url) && (url.Contains("http://") || url.Contains("https://")))
        {
            var uri = new Uri(url);

            var queryParams = HttpUtility.ParseQueryString(uri.Query);

            YoutubeId = queryParams["v"] ?? uri.Segments.Last();
        }
        else
        {
            YoutubeId = string.Empty;
        }
    }

    public override string ToString() => $"Youtube: {YoutubeId}";
}

public record YoutubeBlockDescriptor : BlockDescriptor<YoutubeBlock>
{
    public YoutubeBlockDescriptor(ILocalizationProvider<YoutubeBlock> localizationProvider) : base(
        localizationProvider)
    {
    }
}
