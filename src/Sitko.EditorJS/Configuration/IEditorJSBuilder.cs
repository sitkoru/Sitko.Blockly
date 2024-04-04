using Microsoft.Extensions.Configuration;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Configuration;

public interface IEditorJSBuilder
{
    IEditorJSBuilder AddBlock<TBlock, TBlockOptions>(Action<IConfiguration, TBlockOptions>? configure = null)
        where TBlock : ContentBlock where TBlockOptions : class, IContentBlockOptions<TBlock>;
}
