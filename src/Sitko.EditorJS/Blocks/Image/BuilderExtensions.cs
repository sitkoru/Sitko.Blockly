using Microsoft.Extensions.Configuration;
using Sitko.EditorJS.Configuration;

namespace Sitko.EditorJS.Blocks.Image;

public static class BuilderExtensions
{
    public static IEditorJSBuilder AddImageBlock<TData>(this IEditorJSBuilder builder,
        Action<IConfiguration, ImageBlockOptions<TData>>? configure = null) where TData : class, new() =>
        builder.AddBlock<ImageBlock<TData>, ImageBlockOptions<TData>>(configure);
}
