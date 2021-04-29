using System.Collections.Generic;
using Sitko.Blockly.AntDesign.Forms.Blocks;
using Sitko.Blockly.Blocks;
using Sitko.Core.App;

namespace Sitko.Blockly.AntDesign
{
    public class AntDesignBlocklyModule : BlocklyModule<AntDesignContentBlockDescriptor, AntDesignBlocklyModuleConfig>
    {
        public AntDesignBlocklyModule(AntDesignBlocklyModuleConfig config, Application application) : base(config,
            application)
        {
        }
    }

    public class AntDesignBlocklyModuleConfig : BlocklyModuleConfig<AntDesignContentBlockDescriptor>
    {
        protected override IEnumerable<AntDesignContentBlockDescriptor> GetDefaultBlockDescriptors()
        {
            return new AntDesignContentBlockDescriptor[]
            {
                new AntDesignContentBlockDescriptor<TextBlock, TextBlockForm>("Текст",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"file-text\">")),
                new AntDesignContentBlockDescriptor<CutBlock, CutBlockForm>("Кат",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"scissor\">")),
                new AntDesignContentBlockDescriptor<QuoteBlock, QuoteBlockForm>("Цитата",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"file-text\">")),
                new AntDesignContentBlockDescriptor<FileBlock, FileBlockForm>("Файл",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"paper-clip\">")),
                new AntDesignContentBlockDescriptor<PictureBlock, PictureBlockForm>("Картинка",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"picture\">")),
                new AntDesignContentBlockDescriptor<GalleryBlock, GalleryBlockForm>("Галерея",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"picture\">")),
                new AntDesignContentBlockDescriptor<YoutubeBlock, YoutubeBlockForm>("YouTube",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"youtube\">")),
                new AntDesignContentBlockDescriptor<TwitterBlock, TwitterBlockForm>("Twitter",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"twitter\">")),
                new AntDesignContentBlockDescriptor<TwitchBlock, TwitchBlockForm>("Twitch",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"video-camera\">")),
                new AntDesignContentBlockDescriptor<IframeBlock, IFrameBlockForm>("IFrame",
                    builder => builder.AddMarkupContent(1, "<Icon Type=\"border\">"))
            };
        }
    }
}
