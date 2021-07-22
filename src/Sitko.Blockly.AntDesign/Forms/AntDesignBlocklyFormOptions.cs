using Sitko.Blockly.Blazor.Forms;

namespace Sitko.Blockly.AntDesignComponents.Forms
{
    using Sitko.Blazor.CKEditor;

    public class AntDesignBlocklyFormOptions : BlazorBlocklyFormOptions
    {
        public CKEditorConfig? CKEditorConfig { get; set; }
    }
}
