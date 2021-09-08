using System;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly
{
    using Display;

    public abstract record ContentBlock : IOrdered
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Position { get; set; }
        public bool Enabled { get; set; } = true;

        public override string ToString() => GetType().Name;

        public virtual bool ShouldRender(BlocklyListOptions listOptions) => true;
        public virtual bool ShouldRenderNext(BlocklyListOptions listOptions) => true;
    }
}
