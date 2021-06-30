namespace Sitko.Blockly.Blazor
{
    public interface IBlazorBlockDescriptor<TBlock> : IBlazorBlockDescriptor, IBlockDescriptor<TBlock>
        where TBlock : ContentBlock
    {
    }
}
