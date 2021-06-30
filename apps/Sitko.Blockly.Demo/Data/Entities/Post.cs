using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Sitko.Core.Repository;

namespace Sitko.Blockly.Demo.Data.Entities
{
    public abstract class BaseEntity : IEntity<Guid>
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        [Required] public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.UtcNow;
        [Required] public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;

        public object GetId()
        {
            return Id;
        }
    }

    public class Post : BaseEntity
    {
        public string Title { get; set; } = "";
        public List<ContentBlock> Blocks { get; set; } = new();
        public List<ContentBlock> SecondaryBlocks { get; set; } = new();
    }
}
