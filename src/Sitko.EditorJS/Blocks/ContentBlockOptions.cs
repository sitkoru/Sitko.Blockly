namespace Sitko.EditorJS.Blocks;

public abstract record ContentBlockOptions<TBlock, TConfig> : IContentBlockOptions<TBlock, TConfig>
    where TBlock : ContentBlock
    where TConfig : ContentBlockConfig, new()
{
    public abstract string ScriptUrl { get; set; }
    public abstract string ClassName { get; set; }
    public EditorJSToolConfig GetConfig() => new() { ClassName = ClassName, Config = Config };

    public TConfig Config { get; } = new();
}

public abstract record ContentBlockOptions<TBlock> : ContentBlockOptions<TBlock, ContentBlockConfig>
    where TBlock : ContentBlock;
