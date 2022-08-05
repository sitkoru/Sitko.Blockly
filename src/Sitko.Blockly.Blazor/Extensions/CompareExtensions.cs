using JetBrains.Annotations;
using KellermanSoftware.CompareNetObjects;

namespace Sitko.Blockly.Blazor.Extensions;

[PublicAPI]
public static class CompareExtensions
{
    public static ComparisonConfig AddBlocklyCollectionMapping(this ComparisonConfig config)
    {
        config.IgnoreCollectionOrder = true;
        config.CollectionMatchingSpec ??= new Dictionary<Type, IEnumerable<string>>();
        config.CollectionMatchingSpec.Add(typeof(ContentBlock), new[] { nameof(ContentBlock.Id) });
        return config;
    }
}
