using System;
using System.Collections.Generic;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sitko.Blockly.Blocks;
using Sitko.Core.App;

namespace Sitko.Blockly
{
    public class BlocklyModule<TBlockDescriptor, TConfig> : BaseApplicationModule<TConfig>
        where TBlockDescriptor : ContentBlockDescriptor where TConfig : BlocklyModuleConfig<TBlockDescriptor>, new()
    {
        public BlocklyModule(TConfig config, Application application) : base(config,
            application)
        {
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration,
            IHostEnvironment environment)
        {
            base.ConfigureServices(services, configuration, environment);
            foreach (var descriptor in Config.Descriptors)
            {
                services.AddSingleton(typeof(TBlockDescriptor), descriptor);
            }

            if (Config.WithDefaultFluentValidators)
            {
                services.AddValidatorsFromAssemblyContaining(typeof(BlocklyModule<,>));
            }

            foreach (var validator in Config.Validators)
            {
                services.AddScoped(validator);
                services.AddScoped(typeof(IValidator), validator);
            }

            services.AddSingleton<IBlockly<TBlockDescriptor>, Blockly<TBlockDescriptor>>();
        }
    }

    public abstract class BlocklyModuleConfig<TBlockDescriptor> where TBlockDescriptor : ContentBlockDescriptor
    {
        private readonly HashSet<TBlockDescriptor> _descriptors = new();
        private readonly List<Type> _validators = new();
        public IEnumerable<TBlockDescriptor> Descriptors => _descriptors;
        public IEnumerable<Type> Validators => _validators;

        public bool WithDefaultFluentValidators { get; protected set; } = false;

        public BlocklyModuleConfig<TBlockDescriptor> AddDefaultBlocks()
        {
            foreach (var descriptor in GetDefaultBlockDescriptors())
            {
                RegisterBlock(descriptor);
            }

            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor> AddDefaultFluentValidators()
        {
            WithDefaultFluentValidators = true;
            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor> WithValidatorsFromAssembly()
        {
            return this;
        }

        protected abstract IEnumerable<TBlockDescriptor> GetDefaultBlockDescriptors();

        public BlocklyModuleConfig<TBlockDescriptor> RegisterBlock(TBlockDescriptor descriptor)
        {
            if (_descriptors.Contains(descriptor))
            {
                throw new ArgumentException($"Descriptor for {descriptor.Type} already registered");
            }

            _descriptors.Add(descriptor);
            return this;
        }

        public BlocklyModuleConfig<TBlockDescriptor>
            RegisterBlock<TBlock, TValidator>(TBlockDescriptor descriptor) where TBlock : ContentBlock
            where TValidator : AbstractValidator<TBlock>
        {
            RegisterBlock(descriptor);
            return RegisterBlockValidator<TBlock, TValidator>();
        }

        public BlocklyModuleConfig<TBlockDescriptor>
            RegisterBlockValidator<TBlock, TValidator>() where TBlock : ContentBlock
            where TValidator : AbstractValidator<TBlock>
        {
            _validators.Add(typeof(TValidator));
            return this;
        }
    }

    public class BlocklyModuleConfig : BlocklyModuleConfig<ContentBlockDescriptor>
    {
        protected override IEnumerable<ContentBlockDescriptor> GetDefaultBlockDescriptors()
        {
            return new ContentBlockDescriptor[]
            {
                new ContentBlockDescriptor<TextBlock>("Текст"), 
                new ContentBlockDescriptor<CutBlock>("Кат"),
                new ContentBlockDescriptor<QuoteBlock>("Цитата"),
                new ContentBlockDescriptor<FileBlock>("Файл"),
                new ContentBlockDescriptor<PictureBlock>("Картинка"),
                new ContentBlockDescriptor<GalleryBlock>("Галерея"),
                new ContentBlockDescriptor<YoutubeBlock>("YouTube"),
                new ContentBlockDescriptor<TwitterBlock>("Twitter"),
                new ContentBlockDescriptor<TwitchBlock>("Twitch"),
                new ContentBlockDescriptor<IframeBlock>("IFrame"),
            };
        }
    }
}
