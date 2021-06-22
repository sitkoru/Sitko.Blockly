using System.Collections.Generic;
using AntDesign;
using Microsoft.AspNetCore.Components.Rendering;
using Sitko.Blazor.CKEditor.Bundle;
using Sitko.Blockly.AntDesignComponents.Forms.Blocks;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents
{
    public class AntDesignBlocklyModuleConfig : BlocklyModuleConfig<AntDesignContentBlockDescriptor>
    {
        public CKEditorTheme CKEditorTheme { get; set; } = CKEditorTheme.Light;

        protected override IEnumerable<AntDesignContentBlockDescriptor> GetDefaultBlockDescriptors()
        {
            return new AntDesignContentBlockDescriptor[]
            {
                new AntDesignContentBlockDescriptor<TextBlock, TextBlockForm>("Текст",
                    builder => builder.AddIcon("text")),
                new AntDesignContentBlockDescriptor<CutBlock, CutBlockForm>("Кат",
                    builder => builder.AddIcon("cut")),
                new AntDesignContentBlockDescriptor<QuoteBlock, QuoteBlockForm>("Цитата",
                    builder => builder.AddIcon("quote")),
                new AntDesignContentBlockDescriptor<FileBlock, FileBlockForm>("Файл",
                    builder => builder.AddIcon("attach")),
                new AntDesignContentBlockDescriptor<PictureBlock, PictureBlockForm>("Картинка",
                    builder => builder.AddIcon("image")),
                new AntDesignContentBlockDescriptor<GalleryBlock, GalleryBlockForm>("Галерея",
                    builder => builder.AddIcon("gallery")),
                new AntDesignContentBlockDescriptor<YoutubeBlock, YoutubeBlockForm>("YouTube",
                    builder => builder.AddIcon("youtube")),
                new AntDesignContentBlockDescriptor<TwitterBlock, TwitterBlockForm>("Twitter",
                    builder => builder.AddIcon("twitter")),
                new AntDesignContentBlockDescriptor<TwitchBlock, TwitchBlockForm>("Twitch",
                    builder => builder.AddIcon("twitch")),
                new AntDesignContentBlockDescriptor<IframeBlock, IFrameBlockForm>("IFrame",
                    builder => builder.AddIcon("embed"))
            };
        }
    }

    public static class RenderTreeBuilderExtensions
    {
        public static RenderTreeBuilder AddIcon(this RenderTreeBuilder builder, string icon)
        {
            builder.OpenComponent(1, typeof(Icon));
            builder.AddAttribute(1, nameof(Icon.Component), CustomIconsProvider.GetIcon(icon));
            builder.CloseComponent();
            return builder;
        }
    }
}
