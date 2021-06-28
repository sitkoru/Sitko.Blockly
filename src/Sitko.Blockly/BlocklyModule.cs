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
    public class BlocklyModule : BlocklyModule<ContentBlockDescriptor, BlocklyModuleConfig>
    {
        public BlocklyModule(BlocklyModuleConfig config, Application application) : base(config, application)
        {
        }
    }

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

        public bool WithDefaultFluentValidators { get; protected set; }

        public BlocklyModuleConfig<TBlockDescriptor> AddDefaultBlocks()
        {
            return AddTextBlock()
                .AddCutBlock()
                .AddQuoteBlock()
                .AddFilesBlock()
                .AddGalleryBlock()
                .AddYoutubeBlock()
                .AddTwitterBlock()
                .AddTwitchBlock()
                .AddIframeBlock();
        }

        public BlocklyModuleConfig<TBlockDescriptor> AddTextBlock()
        {
            RegisterBlock(GetTextBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddCutBlock()
        {
            RegisterBlock(GetCutBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddQuoteBlock()
        {
            RegisterBlock(GetQuoteBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddFilesBlock()
        {
            RegisterBlock(GetFilesBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddGalleryBlock()
        {
            RegisterBlock(GetGalleryBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddYoutubeBlock()
        {
            RegisterBlock(GetYoutubeBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddTwitterBlock()
        {
            RegisterBlock(GetTwitterBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddTwitchBlock()
        {
            RegisterBlock(GetTwitchBlockDescriptor());
            return this;
        }


        public BlocklyModuleConfig<TBlockDescriptor> AddIframeBlock()
        {
            RegisterBlock(GetIframeBlockDescriptor());
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

        protected abstract TBlockDescriptor GetTextBlockDescriptor();
        protected abstract TBlockDescriptor GetCutBlockDescriptor();
        protected abstract TBlockDescriptor GetQuoteBlockDescriptor();
        protected abstract TBlockDescriptor GetFilesBlockDescriptor();


        protected abstract TBlockDescriptor GetGalleryBlockDescriptor();
        protected abstract TBlockDescriptor GetYoutubeBlockDescriptor();
        protected abstract TBlockDescriptor GetTwitterBlockDescriptor();
        protected abstract TBlockDescriptor GetTwitchBlockDescriptor();
        protected abstract TBlockDescriptor GetIframeBlockDescriptor();
    }

    public class BlocklyModuleConfig : BlocklyModuleConfig<ContentBlockDescriptor>
    {
        protected override ContentBlockDescriptor GetTextBlockDescriptor()
        {
            return new ContentBlockDescriptor<TextBlock>("Text");
        }

        protected override ContentBlockDescriptor GetCutBlockDescriptor()
        {
            return new ContentBlockDescriptor<CutBlock>("Cut");
        }

        protected override ContentBlockDescriptor GetQuoteBlockDescriptor()
        {
            return new ContentBlockDescriptor<QuoteBlock>("Quote");
        }

        protected override ContentBlockDescriptor GetFilesBlockDescriptor()
        {
            return new ContentBlockDescriptor<FilesBlock>("Files");
        }

        protected override ContentBlockDescriptor GetGalleryBlockDescriptor()
        {
            return new ContentBlockDescriptor<GalleryBlock>("Gallery");
        }

        protected override ContentBlockDescriptor GetYoutubeBlockDescriptor()
        {
            return new ContentBlockDescriptor<YoutubeBlock>("YouTube");
        }

        protected override ContentBlockDescriptor GetTwitterBlockDescriptor()
        {
            return new ContentBlockDescriptor<TwitterBlock>("Twitter");
        }

        protected override ContentBlockDescriptor GetTwitchBlockDescriptor()
        {
            return new ContentBlockDescriptor<TwitchBlock>("Twitch");
        }

        protected override ContentBlockDescriptor GetIframeBlockDescriptor()
        {
            return new ContentBlockDescriptor<IframeBlock>("IFrame");
        }
    }
}
