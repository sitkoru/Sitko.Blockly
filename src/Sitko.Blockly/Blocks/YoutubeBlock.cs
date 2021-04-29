using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Sitko.Blockly.Blocks
{
    public record YoutubeBlock : ContentBlock
    {
        public override string ToString()
        {
            return $"Youtube: {YoutubeId}";
        }

        public string YoutubeId { get; set; } = "";

        [JsonIgnore]
        public string? YoutubeLink
        {
            get
            {
                return string.IsNullOrEmpty(YoutubeId) ? null : $"https://www.youtube.com/watch?v={YoutubeId}";
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && (value.Contains("http://") || value.Contains("https://")))
                {
                    var uri = new Uri(value);

                    var queryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);

                    YoutubeId = queryParams.ContainsKey("v") ? queryParams["v"][0] : uri.Segments.Last();
                }
                else
                {
                    YoutubeId = string.Empty;
                }
            }
        }

        [JsonIgnore]
        public string? EmbedUrl =>
            string.IsNullOrEmpty(YoutubeId) ? null : $"https://www.youtube.com/embed/{YoutubeId}";
    }
}
