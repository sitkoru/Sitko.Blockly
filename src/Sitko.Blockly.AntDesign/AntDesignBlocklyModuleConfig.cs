using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.AntDesignComponents.Display.Blocks;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blazor;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.AntDesignComponents
{
    public class AntDesignBlocklyModuleConfig : BlocklyModuleConfig<BlazorContentBlockDescriptor>
    {
        public CKEditorTheme CKEditorTheme { get; set; } = CKEditorTheme.Light;

        public readonly List<Action<IServiceCollection>> ConfigureServicesActions = new();

        public AntDesignBlocklyModuleConfig ConfigureDefaultStorage<TBlockStorageOptions>()
            where TBlockStorageOptions : class, IBlockStorageOptions
        {
            ConfigureServicesActions.Add(services =>
            {
                services.AddSingleton<IBlockStorageOptions, TBlockStorageOptions>();
            });
            return this;
        }

        public AntDesignBlocklyModuleConfig ConfigureFormStorage<TForm, TBlockFormStorageOptions>()
            where TForm : BaseForm, IBlocklyForm
            where TBlockFormStorageOptions : class, IBlockFormStorageOptions<TForm>
        {
            ConfigureServicesActions.Add(services =>
            {
                services.AddSingleton<IBlockFormStorageOptions<TForm>, TBlockFormStorageOptions>();
            });
            return this;
        }

        protected override BlazorContentBlockDescriptor GetTextBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<TextBlock>("Text",
                builder => builder.AddIcon("text"), typeof(AntTextBlockForm<>), typeof(AntTextBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetCutBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<CutBlock>("Cut",
                builder => builder.AddIcon("cut"), typeof(AntCutBlockForm<>), typeof(AntCutBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetQuoteBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<QuoteBlock>("Quote",
                builder => builder.AddIcon("quote"), typeof(AntQuoteBlockForm<>), typeof(AntQuoteBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetFilesBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<FilesBlock>("Files",
                builder => builder.AddIcon("attach"), typeof(AntFilesBlockForm<>), typeof(AntFilesBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetGalleryBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<GalleryBlock>("Gallery",
                builder => builder.AddIcon("gallery"), typeof(AntGalleryBlockForm<>),
                typeof(AntGalleryBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetYoutubeBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<YoutubeBlock>("YouTube",
                builder => builder.AddIcon("youtube"), typeof(AntYoutubeBlockForm<>),
                typeof(AntYoutubeBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetTwitterBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<TwitterBlock>("Twitter",
                builder => builder.AddIcon("twitter"), typeof(AntTwitterBlockForm<>),
                typeof(AntTwitterBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetTwitchBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<TwitchBlock>("Twitch",
                builder => builder.AddIcon("twitch"), typeof(AntTwitchBlockForm<>), typeof(AntTwitchBlockComponent<>));
        }

        protected override BlazorContentBlockDescriptor GetIframeBlockDescriptor()
        {
            return new BlazorContentBlockDescriptor<IframeBlock>("IFrame",
                builder => builder.AddIcon("embed"), typeof(AntIFrameBlockForm<>), typeof(AntIframeBlockComponent<>));
        }
    }
}
