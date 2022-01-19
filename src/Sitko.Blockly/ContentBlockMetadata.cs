namespace Sitko.Blockly;

public interface IContentBlockMetadata
{
    int Priority { get; }
    int MaxCount { get; }
    Type BlockType { get; }
}

public class ContentBlockMetadata : IContentBlockMetadata
{
    public ContentBlockMetadata(Type blockType, int priority = int.MaxValue, int maxCount = 0)
    {
        Priority = priority;
        MaxCount = maxCount;
        BlockType = blockType;
    }

    public int Priority { get; }
    public int MaxCount { get; }

    public Type BlockType { get; }
}

[AttributeUsage(AttributeTargets.Class)]
public class ContentBlockMetadataAttribute : Attribute
{
    public ContentBlockMetadataAttribute(int priority = int.MaxValue, int maxCount = 0)
    {
        Priority = priority;
        MaxCount = maxCount;
    }

    public int Priority { get; }
    public int MaxCount { get; }
}
