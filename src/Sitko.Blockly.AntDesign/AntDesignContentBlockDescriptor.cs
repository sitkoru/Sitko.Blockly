using System;
using Microsoft.AspNetCore.Components;
using Sitko.Blockly.AntDesign.Forms;

namespace Sitko.Blockly.AntDesign
{
    public record AntDesignContentBlockDescriptor(Type Type, string Title, RenderFragment Icon, Type BlockForm) :
        ContentBlockDescriptor(
            Type,
            Title)
    {
        public RenderFragment RenderBlockForm(ContentBlock block) => builder =>
        {
            builder.OpenComponent(0, BlockForm);
            builder.AddAttribute(1, "Block", block);
            builder.CloseComponent();
        };
    }

    public record AntDesignContentBlockDescriptor<TBlock, TBlockForm>
        (string Title, RenderFragment Icon) : AntDesignContentBlockDescriptor(typeof(TBlock), Title, Icon,
            typeof(TBlockForm))
        where TBlock : ContentBlock where TBlockForm : BaseBlockForm<TBlock>;
}
