using System;
using System.Linq;

namespace Sitko.Blockly.Blocks
{
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

        protected override void ParseUrl(string? url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
                {
                    if (uri.Host == "player.twitch.tv")
                    {
                        var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                        if (queryParams.ContainsKey("collection"))
                        {
                            CollectionId = queryParams["collection"][0];
                            ChannelId = null;
                            if (queryParams.ContainsKey("video"))
                            {
                                VideoId = queryParams["video"][0];
                            }
                        }
                        else if (queryParams.ContainsKey("video"))
                        {
                            VideoId = queryParams["video"][0];
                            CollectionId = null;
                            ChannelId = null;
                        }
                        else if (queryParams.ContainsKey("channel"))
                        {
                            VideoId = null;
                            CollectionId = null;
                            ChannelId = queryParams["channel"][0];
                        }

                        else
                        {
                            VideoId = null;
                            CollectionId = null;
                            ChannelId = null;
                        }
                    }
                    else if ((uri.Host == "twitch.tv" || uri.Host == "www.twitch.tv"))
                    {
                        if (uri.AbsolutePath.StartsWith("/videos"))
                        {
                            VideoId = uri.AbsolutePath.Split('/').Last();
                            CollectionId = null;
                            ChannelId = null;
                        }
                        else if (uri.AbsolutePath.StartsWith("/collections"))
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

        public override string ToString()
        {
            return $"Twitch: {VideoId}{ChannelId}{CollectionId}";
        }

        public string? VideoId { get; set; }
        public string? ChannelId { get; set; }
        public string? CollectionId { get; set; }
    }
}
