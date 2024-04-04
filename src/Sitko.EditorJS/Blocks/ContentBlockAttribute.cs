namespace Sitko.EditorJS.Blocks;

[AttributeUsage(AttributeTargets.Class)]
public class ContentBlockAttribute(string key) : Attribute
{
    public string Key { get; } = key;
}