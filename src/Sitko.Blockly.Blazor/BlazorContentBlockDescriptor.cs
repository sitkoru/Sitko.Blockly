using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.Blazor.Display;
using Sitko.Core.App.Blazor.Forms;
using Sitko.Core.Blazor.FileUpload;
using Sitko.Core.Storage;

namespace Sitko.Blockly.Blazor
{
    public record BlazorContentBlockDescriptor(Type Type, string Title, RenderFragment Icon, Type FormComponent,
        Type DisplayComponent) : ContentBlockDescriptor(
        Type,
        Title)
    {
        public RenderFragment RenderBlockForm<TForm>(TForm form, ContentBlock block)
            where TForm : BaseForm, IBlocklyForm
        {
            return builder =>
            {
                var formType = FormComponent;
                if (FormComponent.IsGenericTypeDefinition)
                {
                    formType = FormComponent.MakeGenericType(typeof(TForm));
                }

                builder.OpenComponent(0, formType);
                builder.AddAttribute(1, "Form", form);
                builder.AddAttribute(2, "Block", block);
                builder.CloseComponent();
            };
        }

        public RenderFragment RenderBlock<TEntity>(ContentBlock block, BlockListContext<TEntity> context)
            where TEntity : IBlocklyEntity
        {
            return builder =>
            {
                var component = DisplayComponent;
                if (FormComponent.IsGenericTypeDefinition)
                {
                    component = DisplayComponent.MakeGenericType(typeof(TEntity));
                }

                builder.OpenComponent(0, component);
                builder.AddAttribute(1, "Block", block);
                builder.AddAttribute(2, "Context", context);
                builder.CloseComponent();
            };
        }
    }

    public record BlazorContentBlockDescriptor<TBlock>
        (string Title, RenderFragment Icon, Type FormComponent, Type DisplayComponent) :
            BlazorContentBlockDescriptor(typeof(TBlock), Title,
                Icon, FormComponent, DisplayComponent) where TBlock : ContentBlock;

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
        where TForm : BaseForm, IBlocklyForm
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

    public interface IBlockFormOptions<TForm> : IBlockOptions where TForm : BaseForm, IBlocklyForm
    {
    }
}
