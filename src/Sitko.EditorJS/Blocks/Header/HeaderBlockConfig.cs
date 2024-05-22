namespace Sitko.EditorJS.Blocks.Header;

public record HeaderBlockConfig : ContentBlockConfig
{
    public string Placeholder { get; set; } = "";
    public string Levels { get; set; } = "[2, 3]";
    public int DefaultLevel { get; set; } = 2;
}
