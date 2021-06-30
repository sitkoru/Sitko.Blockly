using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.Blazor.FileUpload;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blazor
{
    public interface IBlazorBlockDescriptor : IBlockDescriptor
    {
        RenderFragment Icon { get; }
        Type FormComponent { get; }
        Type DisplayComponent { get; }
    }

    public interface IBlazorBlockDescriptor<TBlock> : IBlazorBlockDescriptor, IBlockDescriptor<TBlock>
        where TBlock : ContentBlock
    {
    }

    public interface IBlockFormStorageOptions : IBlockStorageOptions
    {
        string ImagesUploadPath { get; }
        string FilesUploadPath { get; }
        string ImagesTypes { get; }
        string FilesTypes { get; }
        long MaxFileSize { get; }
        long MaxImageSize { get; }
        int? MaxAllowedImages { get; }
        int? MaxAllowedFiles { get; }
        Func<FileUploadRequest, FileStream, Task<object>>? GenerateMetadata { get; }
        Func<FileUploadRequest, FileStream, Task<object>>? GenerateImageMetadata { get; }
    }

    public interface IBlockFormStorageOptions<TForm> : IBlockFormStorageOptions, IBlockFormOptions<TForm>
        where TForm : BaseForm
    {
    }

    public abstract class BlockFormStorageOptions<TStorageOptions> : BlockStorageOptions, IBlockFormStorageOptions
        where TStorageOptions : StorageOptions
    {
        protected BlockFormStorageOptions(IStorage<TStorageOptions> storage) : base(storage)
        {
        }

        public string ImagesUploadPath { get; set; } = "";
        public string FilesUploadPath { get; set; } = "";
        public string ImagesTypes { get; set; } = "image/jpeg,image/png,image/svg+xml";
        public string FilesTypes { get; set; } = "";
        public long MaxFileSize { get; set; }
        public long MaxImageSize { get; set; }
        public int? MaxAllowedImages { get; set; }
        public int? MaxAllowedFiles { get; set; }
        public Func<FileUploadRequest, FileStream, Task<object>>? GenerateMetadata { get; set; }
        public Func<FileUploadRequest, FileStream, Task<object>>? GenerateImageMetadata { get; set; }
    }

    public interface IBlockFormOptions<TForm> : IBlockOptions where TForm : BaseForm
    {
    }
}
