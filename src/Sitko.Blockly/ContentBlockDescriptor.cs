using System;

namespace Sitko.Blockly
{
    public record ContentBlockDescriptor(Type Type, string Title);

    public record ContentBlockDescriptor<TBlock>(string Title) : ContentBlockDescriptor(typeof(TBlock), Title)
        where TBlock : ContentBlock;
}
