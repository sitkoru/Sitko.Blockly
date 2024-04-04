using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Sitko.Blazor.ScriptInjector;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Configuration;

internal class EditorJSBuilder : IEditorJSBuilder
{
    private readonly IServiceCollection serviceCollection;

    public EditorJSBuilder(IServiceCollection serviceCollection)
    {
        this.serviceCollection = serviceCollection;
        serviceCollection.AddOptions<EditorJSOptions>();
        serviceCollection.AddScriptInjector();
        serviceCollection.AddSingleton<IBlocksAccessor, BlocksAccessor>();
    }

    public IEditorJSBuilder AddBlock<TBlock, TBlockOptions>(Action<IConfiguration, TBlockOptions>? configure = null)
        where TBlock : ContentBlock where TBlockOptions : class, IContentBlockOptions<TBlock>
    {
        serviceCollection.Scan(selector =>
            selector.FromType<TBlock>().AsSelfWithInterfaces().WithScopedLifetime());
        serviceCollection.AddOptions<TBlockOptions>().PostConfigure<IConfiguration>((options,
            configuration) =>
        {
            configure?.Invoke(configuration, options);
        });
        serviceCollection.AddSingleton<IContentBlockAccessor, ContentBlockAccessor<TBlock, TBlockOptions>>();
        ContentBlocksRegistry.Register<TBlock, TBlockOptions>();
        return this;
    }
}
