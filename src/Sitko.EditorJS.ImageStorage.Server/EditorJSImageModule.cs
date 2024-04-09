using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Sitko.Core.App;
using Sitko.Core.App.Web;
using Sitko.Core.Storage;
using Sitko.EditorJS.Blocks.Image;

namespace Sitko.EditorJS.Image;

public class EditorJSImageModule<TStorageOptions> : BaseApplicationModule<EditorJSImageModuleOptions<TStorageOptions>>,
    IWebApplicationModule where TStorageOptions : StorageOptions
{
    public override string OptionsKey => "EditorJS:Image";

    public void ConfigureEndpoints(IApplicationContext applicationContext, IApplicationBuilder appBuilder,
        IEndpointRouteBuilder endpoints)
    {
        var options = GetOptions(appBuilder.ApplicationServices);
        endpoints.MapPost(options.UploadFileRoute, async (IFormFile image, IStorage<TStorageOptions> storage) =>
        {
            try
            {
                var item = await storage.SaveAsync(image.OpenReadStream(), image.FileName, "/");
                return ImageUploadResult.Ok(
                    new ImageBlockDataFile<StorageItem> { Url = storage.PublicUri(item).ToString(), Data = item });
            }
            catch (Exception ex)
            {
                return ImageUploadResult.Failed();
            }
        });
    }
}

public class EditorJSImageModuleOptions<TStorageOptions> : BaseModuleOptions where TStorageOptions : StorageOptions
{
    public string UploadFileRoute { get; set; } = "/upload/file";
    public string UploadFileByUrlRoute { get; set; } = "/upload/url";
}
