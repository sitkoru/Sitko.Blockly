namespace Sitko.Blockly.Blocks
{
    public record TextBlock : ContentBlock
    {
        public override string ToString()
        {
            return Text;
        }

        public string Text { get; set; } = "";
    }
}
