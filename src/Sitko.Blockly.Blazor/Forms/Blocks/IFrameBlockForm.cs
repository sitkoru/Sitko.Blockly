using Microsoft.AspNetCore.Components.Forms;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks;

// ReSharper disable once InconsistentNaming
public abstract class IFrameBlockForm<TBlocklyFormOptions> : BlockForm<IframeBlock, TBlocklyFormOptions>
    where TBlocklyFormOptions : BlocklyFormOptions
{
    protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.Src);
}
