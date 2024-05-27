using Microsoft.Extensions.DependencyInjection;
using Sitko.Core.App;
using Sitko.Core.App.Localization;
using Sitko.EditorJS.Blazor.Display;
using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor;

public abstract class EditorJSModule<TBlockDescriptor, TConfig> : BaseApplicationModule<TConfig>
    where TBlockDescriptor : IBlockDescriptor where TConfig : EditorJSModuleOptions<TBlockDescriptor>, new()
{
    public override string OptionsKey => "EditorJS";

    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        TConfig startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        startupOptions.ConfigureServices(services);

        services.AddSingleton<IEditorJS<TBlockDescriptor>, EditorJS<TBlockDescriptor>>();
        services.Configure<JsonLocalizationModuleOptions>(options =>
        {
            options.AddDefaultResource<EditorJS>();
        });
    }

    public override async Task InitAsync(IApplicationContext context, IServiceProvider serviceProvider)
    {
        await base.InitAsync(context, serviceProvider);
        var blockly = serviceProvider.GetRequiredService<IEditorJS<TBlockDescriptor>>();
        await blockly.InitAsync();
    }
}

public abstract class EditorJSModuleOptions<TBlockDescriptor> : BaseModuleOptions
    where TBlockDescriptor : IBlockDescriptor
{
    private readonly List<Action<IServiceCollection>> configureActions = new();

    public void ConfigureServices(IServiceCollection serviceCollection)
    {
        foreach (var action in configureActions)
        {
            action(serviceCollection);
        }
    }

    public EditorJSModuleOptions<TBlockDescriptor> AddBlocks<TAssembly, TDescriptor>(bool withValidators = true)
        where TDescriptor : TBlockDescriptor
    {
        configureActions.Add(services =>
        {
            services.Scan(s =>
                s.FromAssemblyOf<TAssembly>()
                    .AddClasses(c => c.AssignableTo<TDescriptor>().Where(d => !d.IsAbstract && d.IsClass))
                    .AsSelfWithInterfaces().WithSingletonLifetime());
        });
        // if (withValidators)
        // {
        //     AddValidators<TAssembly, IBlockValidator>();
        // }

        return this;
    }

    public EditorJSModuleOptions<TBlockDescriptor> AddBlocks<TAssembly>(bool withValidators = true)
    {
        AddBlocks<TAssembly, TBlockDescriptor>();
        // if (withValidators)
        // {
        //     AddValidators<TAssembly, IBlockValidator>();
        // }

        return this;
    }

    // public EditorJSModuleOptions<TBlockDescriptor> AddValidators<TAssembly, TValidator>()
    //     where TValidator : IBlockValidator
    // {
    //     configureActions.Add(services =>
    //     {
    //         services.Scan(s =>
    //             s
    //                 .FromAssemblyOf<TAssembly>().AddClasses(c => c.AssignableTo<TValidator>())
    //                 .FromAssemblyOf<EditorJS>().AddClasses(c => c.AssignableTo<TValidator>())
    //                 .AsSelfWithInterfaces().WithScopedLifetime());
    //     });
    //     return this;
    // }

    public EditorJSModuleOptions<TBlockDescriptor> AddBlock<TDescriptor, TBlock>(bool withValidator = true)
        where TDescriptor : TBlockDescriptor, IBlockDescriptor<TBlock> where TBlock : ContentBlock
    {
        AddBlocks<TDescriptor, TDescriptor>();
        // if (withValidator)
        // {
        //     AddValidators<TDescriptor, IBlockValidator<TBlock>>();
        // }

        return this;
    }
}

public class EditorJSModuleOptions : EditorJSModuleOptions<IBlockDescriptor>
{
}
