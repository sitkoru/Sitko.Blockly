﻿using Sitko.Core.Blazor.FileUpload;

namespace Sitko.Blockly.Blazor.Forms;

public class BlockFormStorageOptions : BlocklyFormBlockOptions
{
    public string UploadPath { get; set; } = "";
    public string AllowedTypes { get; set; } = "";
    public long MaxFileSize { get; set; }
    public int? MaxAllowedFiles { get; set; }

    public Func<FileUploadRequest, FileStream, Task<object>> GenerateMetadata { get; set; } =
        (_, _) => Task.FromResult((object)null!);
}
