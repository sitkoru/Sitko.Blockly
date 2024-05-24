namespace Sitko.EditorJS.Blocks.Gallery;

public record GalleryBlockOptions<TData> : ContentBlockOptions<GalleryBlock<TData>, GalleryBlockConfig>
    where TData: class, new()
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/editorjs-gallery@latest";
    public override string ClassName { get; set; } = "ImageGallery";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          types: "{{Config.Types}}",
                                                          captionPlaceholder: "{{Config.CaptionPlaceholder}}",
                                                          buttonContent: "{{Config.ButtonContent}}",
                                                          maxElementCount: "{{Config.MaxElementCount}}",
                                                          endpoints: {
                                                            byFile: '{{Config.ByFileUploadUrl}}',
                                                            byUrl: '{{Config.ByUrlUploadUrl}}'
                                                          },
                                                          additionalRequestHeaders: {
                                                            'RequestVerificationToken': window.SitkoEditorJS.antiForgeryToken
                                                          }
                                                          """;
}
