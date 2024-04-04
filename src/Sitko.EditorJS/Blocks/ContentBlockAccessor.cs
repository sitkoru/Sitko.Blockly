using Microsoft.Extensions.Options;

namespace Sitko.EditorJS.Blocks;

public class ContentBlockAccessor<TBlock, TBlockOptions>(IOptions<TBlockOptions> options) : IContentBlockAccessor
    where TBlock : ContentBlock
    where TBlockOptions : class, IContentBlockOptions<TBlock>
{
    public Type Type => typeof(TBlock);
    public IContentBlockOptions Options { get; } = options.Value;
    public string Key { get; } = ContentBlocksRegistry.GetKey<TBlock>().Key;
}