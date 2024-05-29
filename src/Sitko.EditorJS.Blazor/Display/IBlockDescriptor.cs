using Sitko.EditorJS.Blocks;

namespace Sitko.EditorJS.Blazor.Display;

public interface IBlockDescriptor
{
    Type Type { get; }
}

// ReSharper disable once UnusedTypeParameter
public interface IBlockDescriptor<TBlock> : IBlockDescriptor where TBlock : ContentBlock
{
}

public abstract record BlockDescriptor : IBlockDescriptor
{
    public abstract Type Type { get; }
}

public abstract record BlockDescriptor<TBlock> : BlockDescriptor, IBlockDescriptor<TBlock>
    where TBlock : ContentBlock
{
    public override Type Type => typeof(TBlock);
}
