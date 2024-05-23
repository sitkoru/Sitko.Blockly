using Sitko.Core.Repository;
using Sitko.EditorJS.Data;

namespace Sitko.Blockly.Data.Entities;

public abstract record BaseEntityRecord : EntityRecord<Guid>
{
    public override Guid Id { get; set; } = Guid.NewGuid();
    public DateTimeOffset DateAdded { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset DateUpdated { get; set; } = DateTimeOffset.UtcNow;
    public object GetId() => Id;
}

public record Post : BaseEntityRecord
{
    public string Title { get; set; } = "";
    public List<ContentBlock> Blocks { get; set; } = new();
    public List<ContentBlock> SecondaryBlocks { get; set; } = new();
    public EditorJSData EditorJSBlocks { get; set; } = new();
}
