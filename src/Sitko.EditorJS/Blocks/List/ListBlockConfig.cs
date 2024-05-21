namespace Sitko.EditorJS.Blocks.List;

public record ListBlockConfig : ContentBlockConfig
{
    public string DefaultStyle { get; set; } = "ordered";
}
