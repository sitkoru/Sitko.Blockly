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

        protected override AntDesignContentBlockDescriptor GetTextBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<TextBlock, TextBlockForm>("Текст",
                builder => builder.AddIcon("text"));
        }

        protected override AntDesignContentBlockDescriptor GetCutBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<CutBlock, CutBlockForm>("Кат",
                builder => builder.AddIcon("cut"));
        }

        protected override AntDesignContentBlockDescriptor GetQuoteBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<QuoteBlock, QuoteBlockForm>("Цитата",
                builder => builder.AddIcon("quote"));
        }

        protected override AntDesignContentBlockDescriptor GetFileBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<FileBlock, FileBlockForm>("Файл",
                builder => builder.AddIcon("attach"));
        }

        protected override AntDesignContentBlockDescriptor GetPictureBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<PictureBlock, PictureBlockForm>("Картинка",
                builder => builder.AddIcon("image"));
        }

        protected override AntDesignContentBlockDescriptor GetGalleryBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<GalleryBlock, GalleryBlockForm>("Галерея",
                builder => builder.AddIcon("gallery"));
        }

        protected override AntDesignContentBlockDescriptor GetYoutubeBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<YoutubeBlock, YoutubeBlockForm>("YouTube",
                builder => builder.AddIcon("youtube"));
        }

        protected override AntDesignContentBlockDescriptor GetTwitterBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<TwitterBlock, TwitterBlockForm>("Twitter",
                builder => builder.AddIcon("twitter"));
        }

        protected override AntDesignContentBlockDescriptor GetTwitchBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<TwitchBlock, TwitchBlockForm>("Twitch",
                builder => builder.AddIcon("twitch"));
        }

        protected override AntDesignContentBlockDescriptor GetIframeBlockDescriptor()
        {
            return new AntDesignContentBlockDescriptor<IframeBlock, IFrameBlockForm>("IFrame",
                builder => builder.AddIcon("embed"));
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
