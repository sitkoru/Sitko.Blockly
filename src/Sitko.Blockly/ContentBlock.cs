using System;
using System.Collections.Generic;

namespace Sitko.Blockly
{
    public abstract record ContentBlock
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Position { get; set; }

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
        public List<Type>? AllowedBlocks
        {
            get
            {
                return null;
            }
        }
    }

    public abstract class BlocklyEntity : IBlocklyEntity
    {
        public List<ContentBlock> Blocks { get; set; } = new List<ContentBlock>();
    }
}
