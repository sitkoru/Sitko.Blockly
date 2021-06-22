using System.Text.Json.Serialization;

namespace Sitko.Blockly.Blocks
{
    public abstract record UrlContentBlock : ContentBlock
    {
        private string? _currentUrl;
        protected abstract bool IsEmpty { get; }
        protected abstract string FinalUrl { get; }

        [JsonIgnore]
        public string? Url
        {
            get
            {
                return IsEmpty ? _currentUrl : FinalUrl;
            }
            set
            {
                _currentUrl = value;
                ParseUrl(value);
            }
        }

        protected abstract void ParseUrl(string? url);
    }
}
