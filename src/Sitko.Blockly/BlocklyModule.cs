﻿using Microsoft.Extensions.DependencyInjection;
using Sitko.Blockly.Validation;
using Sitko.Core.App;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly;

public class BlocklyModule : BlocklyModule<IBlockDescriptor, BlocklyModuleOptions>
{
}

public class BlocklyModule<TBlockDescriptor, TConfig> : BaseApplicationModule<TConfig>
    where TBlockDescriptor : IBlockDescriptor where TConfig : BlocklyModuleOptions<TBlockDescriptor>, new()
{
    public override string OptionsKey => "Blockly";

    public override void ConfigureServices(IApplicationContext context, IServiceCollection services,
        TConfig startupOptions)
    {
        base.ConfigureServices(context, services, startupOptions);
        startupOptions.ConfigureServices(services);

        services.AddSingleton<IBlockly<TBlockDescriptor>, Blockly<TBlockDescriptor>>();
        services.Configure<JsonLocalizationModuleOptions>(options =>
        {
            options.AddDefaultResource<Blockly>();
        });
    }

    public override async Task InitAsync(IApplicationContext context, IServiceProvider serviceProvider)
    {
        await base.InitAsync(context, serviceProvider);
        var blockly = serviceProvider.GetRequiredService<IBlockly<TBlockDescriptor>>();
        await blockly.InitAsync();
    }
}

public abstract class BlocklyModuleOptions<TBlockDescriptor> : BaseModuleOptions
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

    public BlocklyModuleOptions<TBlockDescriptor> ConfigureFormOptions(Action<BlocklyFormOptions> configure)
    {
        configureActions.Add(services =>
        {
            services.Configure(configure);
        });
        return this;
    }

    public BlocklyModuleOptions<TBlockDescriptor> AddBlocks<TAssembly, TDescriptor>(bool withValidators = true)
        where TDescriptor : TBlockDescriptor
    {
        configureActions.Add(services =>
        {
            services.Scan(s =>
                s.FromAssemblyOf<TAssembly>()
                    .AddClasses(c => c.AssignableTo<TDescriptor>().Where(d => !d.IsAbstract && d.IsClass))
                    .AsSelfWithInterfaces().WithSingletonLifetime());
        });
        if (withValidators)
        {
            AddValidators<TAssembly, IBlockValidator>();
        }

        return this;
    }

    public BlocklyModuleOptions<TBlockDescriptor> AddBlocks<TAssembly>(bool withValidators = true)
    {
        AddBlocks<TAssembly, TBlockDescriptor>();
        if (withValidators)
        {
            AddValidators<TAssembly, IBlockValidator>();
        }

        return this;
    }

    public BlocklyModuleOptions<TBlockDescriptor> AddValidators<TAssembly, TValidator>()
        where TValidator : IBlockValidator
    {
        configureActions.Add(services =>
        {
            services.Scan(s =>
                s
                    .FromAssemblyOf<TAssembly>().AddClasses(c => c.AssignableTo<TValidator>())
                    .FromAssemblyOf<Blockly>().AddClasses(c => c.AssignableTo<TValidator>())
                    .AsSelfWithInterfaces().WithScopedLifetime());
        });
        return this;
    }

    public BlocklyModuleOptions<TBlockDescriptor> AddBlock<TDescriptor, TBlock>(bool withValidator = true)
        where TDescriptor : TBlockDescriptor, IBlockDescriptor<TBlock> where TBlock : ContentBlock
    {
        AddBlocks<TDescriptor, TDescriptor>();
        if (withValidator)
        {
            AddValidators<TDescriptor, IBlockValidator<TBlock>>();
        }

        return this;
    }
}

public class BlocklyModuleOptions : BlocklyModuleOptions<IBlockDescriptor>
{
}
