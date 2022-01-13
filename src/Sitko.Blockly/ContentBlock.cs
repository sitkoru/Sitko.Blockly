using Sitko.Blockly.Display;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly;

public abstract record ContentBlock : IOrdered
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public bool Enabled { get; set; } = true;
    public int Position { get; set; }

    public override string ToString() => GetType().Name;

    public virtual bool ShouldRender(BlocklyListOptions listOptions) => true;
    public virtual bool ShouldRenderNext(BlocklyListOptions listOptions) => true;
}
