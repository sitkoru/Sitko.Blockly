using System.Text.Json.Serialization;

namespace Sitko.Blockly.Blocks;

public abstract record UrlContentBlock : ContentBlock
{
    private string? currentUrl;
    protected abstract bool IsEmpty { get; }
    protected abstract string FinalUrl { get; }

    [JsonIgnore]
    public string? Url
    {
        get => IsEmpty ? currentUrl : FinalUrl;
        set
        {
            currentUrl = value;
            ParseUrl(value);
        }
    }

    protected abstract void ParseUrl(string? url);
}
