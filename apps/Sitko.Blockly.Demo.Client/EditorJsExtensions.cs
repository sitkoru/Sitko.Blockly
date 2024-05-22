using Sitko.Core.Storage;
using Sitko.EditorJS;
using Sitko.EditorJS.Blocks.Header;
using Sitko.EditorJS.Blocks.Image;
using Sitko.EditorJS.Blocks.List;
using Sitko.EditorJS.Blocks.Paragraph;
using Sitko.EditorJS.Blocks.Quote;
using Sitko.EditorJS.Blocks.SimpleImage;
using Sitko.EditorJS.Blocks.Table;

namespace Sitko.Blockly.Demo.Client;

public static class EditorJsExtensions
{
    public static IServiceCollection AddEditorJSBlocks(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddEditorJS()
            .AddBlock<QuoteBlock, QuoteBlockOptions>()
            .AddBlock<TableBlock, TableBlockOptions>()
            .AddBlock<ListBlock, ListBlockOptions>()
            .AddBlock<ParagraphBlock, ParagraphBlockOptions>()
            .AddBlock<HeaderBlock, HeaderBlockOptions>()
            .AddBlock<SimpleImageBlock, SimpleImageBlockOptions>()
            .AddImageBlock<StorageItem>();
        return serviceCollection;
    }
}
