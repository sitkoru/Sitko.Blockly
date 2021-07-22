using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Sitko.Core.Storage;

namespace Sitko.Blockly.HtmlParser
{
    public class FilesUploader<TStorageOptions> where TStorageOptions : StorageOptions
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IStorage<TStorageOptions> storage;
        private readonly ILogger<FilesUploader<TStorageOptions>> logger;
        private readonly Func<string, HttpResponseHeaders, MemoryStream, Task<object>>? generateMetadata;

        public FilesUploader(IHttpClientFactory httpClientFactory, IStorage<TStorageOptions> storage,
            ILogger<FilesUploader<TStorageOptions>> logger,
            Func<string, HttpResponseHeaders, MemoryStream, Task<object>>? generateMetadata = null)
        {
            this.httpClientFactory = httpClientFactory;
            this.storage = storage;
            this.logger = logger;
            this.generateMetadata = generateMetadata;
        }

        public async Task<StorageItem?> UploadFromUrlAsync(string url, string path, string? fileName = null)
        {
            fileName ??= Path.GetFileName(url);
            if (!string.IsNullOrEmpty(fileName))
            {
                if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || string.IsNullOrEmpty(uri.Host))
                {
                    logger.LogInformation("Add base domain to url");
                }
                else
                {
                    logger.LogInformation("Url is ok");
                }

                logger.LogInformation("Downloading file from url {Url}", url);
                try
                {
                    var fileData = await httpClientFactory.CreateClient().GetAsync(url);
                    if (fileData.IsSuccessStatusCode)
                    {
                        var memoryStream = new MemoryStream();
                        var content = await fileData.Content.ReadAsStreamAsync();
                        await content.CopyToAsync(memoryStream);
                        object? metadata = null;
                        if (generateMetadata is not null)
                        {
                            metadata = await generateMetadata(fileName, fileData.Headers, memoryStream);
                        }

                        var item = await storage.SaveAsync(memoryStream, fileName, path, metadata);
                        return item;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error while uploading file from url: {Url}: {ErrorText}", url, ex.ToString());
                }
            }

            return null;
        }
    }
}
