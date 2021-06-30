using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Blockly.Validation;
using Sitko.Core.App;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly
{
    public class BlocklyModule : BlocklyModule<IBlockDescriptor, BlocklyModuleConfig>
    {
        public BlocklyModule(BlocklyModuleConfig config, Application application) : base(config, application)
        {
        }
    }

    public class BlocklyModule<TBlockDescriptor, TConfig> : BaseApplicationModule<TConfig>
        where TBlockDescriptor : IBlockDescriptor where TConfig : BlocklyModuleConfig<TBlockDescriptor>, new()
    {
        public BlocklyModule(TConfig config, Application application) : base(config,
            application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            Config.ConfigureServices(services);

            services.AddSingleton<IBlockly<TBlockDescriptor>, Blockly<TBlockDescriptor>>();
            services.Configure<JsonStringLocalizerOptions>(options =>
            {
                options.AddDefaultResource<Blockly>();
            });
        }
    }

    public abstract class BlocklyModuleConfig<TBlockDescriptor> where TBlockDescriptor : IBlockDescriptor
    {
        private readonly List<Action<IServiceCollection>> _configureActions = new();

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            foreach (var action in _configureActions)
            {
                action(serviceCollection);
            }
        }

        public BlocklyModuleConfig<TBlockDescriptor> ConfigureFormOptions(Action<BlocklyFormOptions> configure)
        {
            _configureActions.Add(services =>
            {
                services.Configure(configure);
            });
            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor> AddBlocks<TAssembly, TDescriptor>(bool withValidators = true)
            where TDescriptor : TBlockDescriptor
        {
            _configureActions.Add(services =>
            {
                services.Scan(s =>
                    s.FromAssemblyOf<TAssembly>().AddClasses(c => c.AssignableTo<TDescriptor>())
                        .AsSelfWithInterfaces().WithSingletonLifetime());
            });
            if (withValidators)
            {
                AddValidators<TAssembly, IBlockValidator>();
            }

            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor> AddBlocks<TAssembly>(bool withValidators = true)
        {
            AddBlocks<TAssembly, TBlockDescriptor>();
            if (withValidators)
            {
                AddValidators<TAssembly, IBlockValidator>();
            }

            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor> AddValidators<TAssembly, TValidator>()
            where TValidator : IBlockValidator
        {
            _configureActions.Add(services =>
            {
                services.Scan(s =>
                    s.FromAssemblyOf<TAssembly>().FromAssemblyOf<Blockly>()
                        .AddClasses(c => c.AssignableTo<TValidator>())
                        .AsSelfWithInterfaces().WithScopedLifetime());
            });
            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor> AddBlock<TDescriptor, TBlock>(bool withValidator = true)
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

    public class BlocklyModuleConfig : BlocklyModuleConfig<IBlockDescriptor>
    {
    }
}
