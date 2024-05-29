using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;
using Sitko.EditorJS.Blocks.Header;
using Sitko.EditorJS.Blocks.Image;
using Sitko.EditorJS.Blocks.List;
using Sitko.EditorJS.Blocks.Marker;
using Sitko.EditorJS.Blocks.Paragraph;
using Sitko.EditorJS.Blocks.Quote;
using Sitko.EditorJS.Blocks.SimpleImage;
using Sitko.EditorJS.Blocks.Strikethrough;
using Sitko.EditorJS.Blocks.Table;
using Sitko.EditorJS.Blocks.Underline;
using Sitko.EditorJS.Configuration;

[assembly: InternalsVisibleTo("Sitko.EditorJS.Tests")]
namespace Sitko.EditorJS;

public static class ServiceCollectionExtensions
{
    public static IEditorJSBuilder AddEditorJS(this IServiceCollection serviceCollection)
    {
        var builder = new EditorJSBuilder(serviceCollection);
        return builder;
    }

    public static IEditorJSBuilder AddDefaultBlocks<TImageData>(this IEditorJSBuilder builder)
        where TImageData : class, new() =>
         builder.AddBlock<StrikethroughBlock, StrikethroughBlockOptions>()
            .AddBlock<MarkerBlock, MarkerBlockOptions>()
            .AddBlock<UnderlineBlock, UnderlineBlockOptions>()
            .AddBlock<QuoteBlock, QuoteBlockOptions>()
            .AddBlock<TableBlock, TableBlockOptions>()
            .AddBlock<ListBlock, ListBlockOptions>()
            .AddBlock<ParagraphBlock, ParagraphBlockOptions>()
            .AddBlock<HeaderBlock, HeaderBlockOptions>()
            .AddBlock<SimpleImageBlock, SimpleImageBlockOptions>()
            .AddGalleryBlock<TImageData>()
            .AddImageBlock<TImageData>();

}
