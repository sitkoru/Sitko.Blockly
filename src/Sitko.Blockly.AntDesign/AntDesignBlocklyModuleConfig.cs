using Sitko.Blockly.Blazor;

namespace Sitko.Blockly.AntDesignComponents
{
    public class AntDesignBlocklyModuleConfig : BlocklyModuleConfig<IBlazorBlockDescriptor>
    {
        public AntDesignBlocklyTheme Theme { get; set; } = AntDesignBlocklyTheme.Light;
    }

    public enum AntDesignBlocklyTheme
    {
        Light,
        Dark
    }
}
