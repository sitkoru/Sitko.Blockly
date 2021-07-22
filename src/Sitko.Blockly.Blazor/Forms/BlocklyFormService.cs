using System.Collections.Generic;

namespace Sitko.Blockly.Blazor.Forms
{
    public class BlocklyFormService
    {
        private readonly List<IBlocklyForm> forms = new();

        public void AddForm(IBlocklyForm blocklyForm) => forms.Add(blocklyForm);

        public void RemoveForm(IBlocklyForm blocklyForm) => forms.Remove(blocklyForm);

        public void Validate()
        {
            foreach (var form in forms)
            {
                form.ValidateBlocks();
            }
        }
    }
}
