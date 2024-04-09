using JetBrains.Annotations;
using Microsoft.Extensions.Hosting;
using Sitko.Core.App;
using Sitko.Core.Storage;

namespace Sitko.EditorJS.Image;

[PublicAPI]
public static class ApplicationExtensions
{
    public static IHostApplicationBuilder AddEditorJSImage<TStorageOptions>(
        this IHostApplicationBuilder hostApplicationBuilder,
        Action<IApplicationContext, EditorJSImageModuleOptions<TStorageOptions>> configure, string? optionsKey = null)
        where TStorageOptions : StorageOptions
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreApplicationBuilder>()
            .AddEditorJSImage(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static IHostApplicationBuilder AddEditorJSImage<TStorageOptions>(
        this IHostApplicationBuilder hostApplicationBuilder,
        Action<EditorJSImageModuleOptions<TStorageOptions>>? configure = null, string? optionsKey = null)
        where TStorageOptions : StorageOptions
    {
        hostApplicationBuilder.GetSitkoCore<ISitkoCoreApplicationBuilder>()
            .AddEditorJSImage(configure, optionsKey);
        return hostApplicationBuilder;
    }

    public static ISitkoCoreApplicationBuilder AddEditorJSImage<TStorageOptions>(
        this ISitkoCoreApplicationBuilder applicationBuilder,
        Action<IApplicationContext, EditorJSImageModuleOptions<TStorageOptions>> configure,
        string? configKey = null) where TStorageOptions : StorageOptions
    {
        applicationBuilder
            .AddModule<EditorJSImageModule<TStorageOptions>, EditorJSImageModuleOptions<TStorageOptions>>(configure,
                configKey);
        return applicationBuilder;
    }

    public static ISitkoCoreApplicationBuilder AddEditorJSImage<TStorageOptions>(
        this ISitkoCoreApplicationBuilder applicationBuilder,
        Action<EditorJSImageModuleOptions<TStorageOptions>>? configure = null, string? configKey = null)
        where TStorageOptions : StorageOptions
    {
        applicationBuilder
            .AddModule<EditorJSImageModule<TStorageOptions>, EditorJSImageModuleOptions<TStorageOptions>>(configure,
                configKey);
        return applicationBuilder;
    }
}
