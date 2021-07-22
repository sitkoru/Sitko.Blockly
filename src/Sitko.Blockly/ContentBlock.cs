using System;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly
{
    public abstract record ContentBlock : IOrdered
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Position { get; set; }
        public bool Enabled { get; set; } = true;

        public override string ToString() => GetType().Name;
    }
}
