using Sitko.Blockly.Blazor;

namespace Sitko.Blockly.MudBlazor;

public class MudBlazorBlocklyModuleOptions : BlazorBlocklyModuleOptions<IBlazorBlockDescriptor>
{
    public MudBlazorBlocklyTheme Theme { get; set; } =MudBlazorBlocklyTheme.Light;
}

public enum MudBlazorBlocklyTheme
{
    Light,
    Dark
}
