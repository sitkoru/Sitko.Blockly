using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sitko.Blockly.Blocks
{
    public record TwitchBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"Twitch: {VideoId}{ChannelId}{CollectionId}";
        }

        public string? VideoId { get; set; }
        public string? ChannelId { get; set; }
        public string? CollectionId { get; set; }

        [JsonIgnore]
        public string? TwitchLink
        {
            get
            {
                var url = "https://player.twitch.tv/";
                if (!string.IsNullOrEmpty(VideoId))
                {
                    url += $"?video={VideoId}";
                }
                else if (!string.IsNullOrEmpty(CollectionId))
                {
                    url += $"?collection={CollectionId}";
                }
                else if (!string.IsNullOrEmpty(ChannelId))
                {
                    url += $"?channel={ChannelId}";
                }
                else
                {
                    return null;
                }

                return url;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (Uri.TryCreate(value, UriKind.Absolute, out var uri))
                    {
                        if (uri.Host == "player.twitch.tv")
                        {
                            var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
                            if (queryParams.ContainsKey("video"))
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
                            else if (queryParams.ContainsKey("collection"))
                            {
                                VideoId = null;
                                CollectionId = queryParams["collection"][0];
                                ChannelId = null;
                            }
                            else
                            {
                                VideoId = null;
                                CollectionId = null;
                                ChannelId = null;
                            }
                        }
                        else if ((uri.Host == "twitch.tv" || uri.Host == "www.twitch.tv") &&
                                 uri.AbsolutePath.StartsWith("/videos"))
                        {
                            VideoId = uri.AbsolutePath.Split('/').Last();
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
                else
                {
                    VideoId = null;
                    CollectionId = null;
                    ChannelId = null;
                }
            }
        }
    }
}
