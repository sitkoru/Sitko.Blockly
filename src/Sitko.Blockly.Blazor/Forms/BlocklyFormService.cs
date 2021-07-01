using System.Collections.Generic;

namespace Sitko.Blockly.Blazor.Forms
{
    public class BlocklyFormService
    {
        private readonly List<IBlocklyForm> _forms = new();

        public void AddForm(IBlocklyForm blocklyForm)
        {
            _forms.Add(blocklyForm);
        }

        public void RemoveForm(IBlocklyForm blocklyForm)
        {
            _forms.Remove(blocklyForm);
        }

        public void Validate()
        {
            foreach (var form in _forms)
            {
                form.ValidateBlocks();
            }
        }
    }
}
