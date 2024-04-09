namespace Sitko.EditorJS.Blocks.Image;

public record ImageBlockOptions<TData> : ContentBlockOptions<ImageBlock<TData>, ImageBlockConfig>
    where TData : class, new()
{
    public override string ScriptUrl { get; set; } = "https://cdn.jsdelivr.net/npm/@editorjs/image@latest";
    public override string ClassName { get; set; } = "ImageTool";

    protected override string GetToolConfig(Guid id) => $$"""
                                                          types: "{{Config.Types}}",
                                                          captionPlaceholder: "{{Config.CaptionPlaceholder}}",
                                                          buttonContent: "{{Config.ButtonContent}}",
                                                          endpoints: {
                                                            byFile: '{{Config.ByFileUploadUrl}}',
                                                            byUrl: '{{Config.ByUrlUploadUrl}}'
                                                          },
                                                          additionalRequestHeaders: {
                                                            'RequestVerificationToken': window.SitkoEditorJS.antiForgeryToken
                                                          }
                                                          """;
    // for better times
    // uploader: {
    //     uploadByFile(file){
    //         return window.SitkoEditorJS.uploadFile('{{id}}', file);
    //     },
    //     uploadByUrl(url){
    //         return window.SitkoEditorJS.uploadFileFromUrl('{{id}}', url);
    //     }
    // }
}
