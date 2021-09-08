namespace Sitko.Blockly
{
    using System;

    public interface IContentBlockMetadata
    {
        int Priority { get; }
        int MaxCount { get; }
        Type BlockType { get; }
    }

    public class ContentBlockMetadata : IContentBlockMetadata
    {
        public int Priority { get; }
        public int MaxCount { get; }

        public Type BlockType { get; }

        public ContentBlockMetadata(Type blockType, int priority = int.MaxValue, int maxCount = 0)
        {
            Priority = priority;
            MaxCount = maxCount;
            BlockType = blockType;
        }
    }

    public class ContentBlockMetadataAttribute : Attribute
    {
        public int Priority { get; }
        public int MaxCount { get; }

        public ContentBlockMetadataAttribute(int priority = int.MaxValue, int maxCount = 0)
        {
            Priority = priority;
            MaxCount = maxCount;
        }
    }
}
