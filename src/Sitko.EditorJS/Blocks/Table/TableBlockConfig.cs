namespace Sitko.EditorJS.Blocks.Table;

public record TableBlockConfig : ContentBlockConfig
{
    public int Rows { get; set; } = 2;
    public int Cols { get; set; } = 2;
    public bool WithHeadings { get; set; }
}
