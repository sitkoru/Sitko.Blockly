namespace Sitko.Blockly.Blocks
{
    public record CutBlock : ContentBlock

    {
        public override string ToString()
        {
            return "";
        }

        public string ButtonText { get; set; } = "Read more...";
    }
}
