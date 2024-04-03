using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Scrutor;
using Sitko.Blazor.ScriptInjector;

namespace Sitko.EditorJS;

public static class HostApplicationBuilderExtensions
{
    public static IEditorJSBuilder AddEditorJS(this IServiceCollection serviceCollection)
    {
        var builder = new EditorJSBuilder(serviceCollection);
        return builder;
    }
}

public interface IEditorJSBuilder
{
    IEditorJSBuilder AddBlock<TBlock, TBlockOptions>(Action<IConfiguration, TBlockOptions> configure)
        where TBlock : ContentBlock where TBlockOptions : class, IContentBlockOptions<TBlock>;
}

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

    public IEditorJSBuilder AddBlock<TBlock, TBlockOptions>(Action<IConfiguration, TBlockOptions> configure)
        where TBlock : ContentBlock where TBlockOptions : class, IContentBlockOptions<TBlock>
    {
        serviceCollection.Scan(selector =>
            selector.FromType<TBlock>().AsSelfWithInterfaces().WithScopedLifetime());
        serviceCollection.AddOptions<TBlockOptions>().PostConfigure<IConfiguration>((options,
            configuration) =>
        {
            configure(configuration, options);
        });
        serviceCollection.AddSingleton<IBlockOptionsAccessor, BlockOptionsAccessor<TBlockOptions>>();
        return this;
    }
}

public interface IBlocksAccessor
{
    EditorJSConfig GetConfig(string holder);
    IReadOnlyCollection<(string Key, string ScriptUrl)> GetScripts();
}

internal class BlocksAccessor : IBlocksAccessor
{
    private readonly List<ContentBlock> blocks;
    private readonly List<IBlockOptionsAccessor> blockOptionsAccessors;

    public BlocksAccessor(IEnumerable<ContentBlock> blocks, IEnumerable<IBlockOptionsAccessor> blockOptionsAccessors)
    {
        this.blocks = blocks.ToList();
        this.blockOptionsAccessors = blockOptionsAccessors.ToList();
    }

    public EditorJSConfig GetConfig(string holder)
    {
        var config = new EditorJSConfig { Holder = holder };
        foreach (var blockOptionsAccessor in blockOptionsAccessors)
        {
            config.Tools[blockOptionsAccessor.Current.Type] = blockOptionsAccessor.Current.GetConfig();
        }

        return config;
    }

    public IReadOnlyCollection<(string Key, string ScriptUrl)> GetScripts() => blockOptionsAccessors
        .Select(accessor => (accessor.Current.Type, accessor.Current.ScriptUrl)).ToArray();
}

public interface IBlockOptionsAccessor
{
    IContentBlockOptions Current { get; }
}

public class BlockOptionsAccessor<TOptions> : IBlockOptionsAccessor where TOptions : class, IContentBlockOptions
{
    private readonly IOptions<TOptions> options;

    public BlockOptionsAccessor(IOptions<TOptions> options) => this.options = options;

    public IContentBlockOptions Current => options.Value;
}

public interface IContentBlockOptions
{
    string ScriptUrl { get; }
    string Type { get; }
    EditorJSToolConfig GetConfig();
}

public interface IContentBlockOptions<TBlock> : IContentBlockOptions where TBlock : ContentBlock
{
}

public interface IContentBlockOptions<TBlock, TConfig> : IContentBlockOptions<TBlock>
    where TConfig : ContentBlockConfig, new() where TBlock : ContentBlock
{
    TConfig Config { get; }
}

public record ContentBlockConfig
{
}

public abstract record ContentBlockOptions<TBlock, TConfig> : IContentBlockOptions<TBlock, TConfig>
    where TBlock : ContentBlock
    where TConfig : ContentBlockConfig, new()
{
    public abstract string ScriptUrl { get; set; }
    public abstract string Type { get; set; }
    public abstract string ClassName { get; set; }
    public EditorJSToolConfig GetConfig() => new() { ClassName = ClassName, Config = Config };

    public TConfig Config { get; } = new();
}

public abstract record ContentBlockOptions<TBlock> : ContentBlockOptions<TBlock, ContentBlockConfig>
    where TBlock : ContentBlock
{
}

public record EditorJSOptions
{
    public string EditorJSScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/editorjs@latest";
}

public interface IEditorJSTool
{
}

public interface IEditorJSTool<TBlock> : IEditorJSTool where TBlock : ContentBlock
{
}

public abstract record ContentBlock
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

public record SimpleImageBlock : ContentBlock
{
}

public record SimpleImageBlockOptions : ContentBlockOptions<SimpleImageBlock>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/simple-image@latest";
    public override string Type { get; set; } = "image";
    public override string ClassName { get; set; } = "SimpleImage";
}

public record ParagraphBlock : ContentBlock
{
}

public record ParagraphBlockOptions : ContentBlockOptions<ParagraphBlock, ParagraphBlockConfig>
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/paragraph@latest";
    public override string Type { get; set; } = "paragraph";
    public override string ClassName { get; set; } = "Paragraph";
}

public record ParagraphBlockConfig : ContentBlockConfig
{
    [JsonPropertyName("placeholder")] public string Placeholder { get; set; } = "";

    [JsonPropertyName("preserveBlank")] public bool PreserveBlank { get; set; } = false;
}
