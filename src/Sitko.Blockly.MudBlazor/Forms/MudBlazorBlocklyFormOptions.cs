﻿using Sitko.Blazor.CKEditor;
using Sitko.Blockly.Blazor.Forms;

namespace Sitko.Blockly.MudBlazorComponents.Forms;

public class MudBlazorBlocklyFormOptions : BlazorBlocklyFormOptions
{
    public CKEditorConfig? CKEditorConfig { get; set; }
}
