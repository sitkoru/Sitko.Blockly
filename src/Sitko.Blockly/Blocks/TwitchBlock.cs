using System.Web;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.Blocks;

[ContentBlockMetadata(8)]
public record TwitchBlock : UrlContentBlock
{
    protected override bool IsEmpty => string.IsNullOrEmpty(VideoId) && string.IsNullOrEmpty(ChannelId) &&
                                       string.IsNullOrEmpty(CollectionId);

    protected override string FinalUrl
    {
        get
        {
            var url = "https://www.twitch.tv/";
            if (!string.IsNullOrEmpty(CollectionId))
            {
                if (!string.IsNullOrEmpty(VideoId))
                {
                    url += $"videos/{VideoId}?collection={CollectionId}&filter=collections";
                }
                else
                {
                    url += $"collections/{CollectionId}?filter=collections";
                }

                return url;
            }

            if (!string.IsNullOrEmpty(ChannelId))
            {
                url += ChannelId;
                return url;
            }

            url += $"videos/{VideoId}";

            return url;
        }
    }

    public string? VideoId { get; set; }
    public string? ChannelId { get; set; }
    public string? CollectionId { get; set; }

    protected override void ParseUrl(string? url)
    {
        if (!string.IsNullOrEmpty(url))
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                if (uri.Host == "player.twitch.tv")
                {
                    var queryParams = HttpUtility.ParseQueryString(uri.Query);
                    if (queryParams.AllKeys.Contains("collection"))
                    {
                        CollectionId = queryParams["collection"];
                        ChannelId = null;
                        if (queryParams.AllKeys.Contains("video"))
                        {
                            VideoId = queryParams["video"];
                        }
                    }
                    else if (queryParams.AllKeys.Contains("video"))
                    {
                        VideoId = queryParams["video"];
                        CollectionId = null;
                        ChannelId = null;
                    }
                    else if (queryParams.AllKeys.Contains("channel"))
                    {
                        VideoId = null;
                        CollectionId = null;
                        ChannelId = queryParams["channel"];
                    }

                    else
                    {
                        VideoId = null;
                        CollectionId = null;
                        ChannelId = null;
                    }
                }
                else if (uri.Host == "twitch.tv" || uri.Host == "www.twitch.tv")
                {
                    if (uri.AbsolutePath.StartsWith("/videos", StringComparison.InvariantCulture))
                    {
                        VideoId = uri.AbsolutePath.Split('/').Last();
                        CollectionId = null;
                        ChannelId = null;
                    }
                    else if (uri.AbsolutePath.StartsWith("/collections", StringComparison.InvariantCulture))
                    {
                        VideoId = null;
                        CollectionId = uri.AbsolutePath.Split('/').Last();
                        ChannelId = null;
                    }
                    else if (uri.AbsolutePath.Split('/').Length == 2)
                    {
                        VideoId = null;
                        CollectionId = null;
                        ChannelId = uri.AbsolutePath.Split('/').Last();
                    }
                }
            }
            else
            {
                VideoId = null;
                CollectionId = null;
                ChannelId = null;
            }
        }
        else
        {
            VideoId = null;
            CollectionId = null;
            ChannelId = null;
        }
    }

    public override string ToString() => $"Twitch: {VideoId}{ChannelId}{CollectionId}";
}

public record TwitchBlockDescriptor : BlockDescriptor<TwitchBlock>
{
    public TwitchBlockDescriptor(ILocalizationProvider<TwitchBlock> localizationProvider) : base(
        localizationProvider)
    {
    }
}
