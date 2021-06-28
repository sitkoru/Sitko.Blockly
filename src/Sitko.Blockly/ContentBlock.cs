using System;
using System.Collections.Generic;
using Sitko.Core.App.Collections;

namespace Sitko.Blockly
{
    public abstract record ContentBlock : IOrdered
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Position { get; set; }
        public bool Enabled { get; set; } = true;

        public override string ToString()
        {
            return GetType().Name;
        }
    }

    public interface IBlocklyEntity
    {
        List<ContentBlock> Blocks { get; set; }
    }

    public interface IBlocklyForm : IBlocklyEntity
    {
#if NETCOREAPP3_1
        public List<Type>? AllowedBlocks { get; }
#else
        public List<Type>? AllowedBlocks
        {
            get
            {
                return null;
            }
        }
#endif
    }

    public abstract class BlocklyEntity : IBlocklyEntity
    {
        public List<ContentBlock> Blocks { get; set; } = new();
    }
}
