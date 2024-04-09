using Sitko.Core.Storage;
using Sitko.EditorJS;
using Sitko.EditorJS.Blocks.Image;
using Sitko.EditorJS.Blocks.Paragraph;
using Sitko.EditorJS.Blocks.SimpleImage;

namespace Sitko.Blockly.Demo.Client;

public static class EditorJsExtensions
{
    public static IServiceCollection AddEditorJSBlocks(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEditorJS()
            .AddBlock<ParagraphBlock, ParagraphBlockOptions>()
            .AddBlock<SimpleImageBlock, SimpleImageBlockOptions>()
            .AddImageBlock<StorageItem>();
        return serviceCollection;
    }
}
