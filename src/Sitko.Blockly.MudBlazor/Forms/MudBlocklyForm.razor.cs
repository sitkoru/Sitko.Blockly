using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Sitko.Core.App.Localization;

namespace Sitko.Blockly.MudBlazorComponents.Forms;

public partial class MudBlocklyForm
{
    [Inject] protected ILocalizationProvider<MudBlocklyForm> LocalizationProvider { get; set; } = null!;

    [Inject] protected IOptions<MudBlazorBlocklyModuleOptions> ModuleOptions { get; set; } = null!;

    [Parameter] public string? Label { get; set; }

    private Guid? _openBlockId;

    private void ToggleOpen(Guid blockId)
    {
        if (_openBlockId == blockId)
        {
            _openBlockId = null;
        }
        else
        {
            _openBlockId = blockId;
        }
    }

}
