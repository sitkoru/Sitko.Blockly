namespace Sitko.EditorJS.Blocks;

public interface IContentBlockOptions
{
    string ScriptUrl { get; }
    string GetConfig(Guid id);
}

public interface IContentBlockOptions<TBlock> : IContentBlockOptions where TBlock : ContentBlock;

public interface IContentBlockOptions<TBlock, out TConfig> : IContentBlockOptions<TBlock>
    where TConfig : ContentBlockConfig, new() where TBlock : ContentBlock
{
    TConfig Config { get; }
}
