namespace Sitko.Blockly.Blazor.Helpers
{
    using Sitko.Blazor.ScriptInjector;

    public class JsHelper
    {
        public static ScriptInjectRequest TwitchScriptRequest { get; } = ScriptInjectRequest.FromUrl(
            "blocklyTwitch",
            "/_content/Sitko.Blockly.Blazor/blocklyTwitch.js");

        public static ScriptInjectRequest TwitterScriptRequest { get; } = ScriptInjectRequest.FromUrl(
            "blocklyTwitter",
            "/_content/Sitko.Blockly.Blazor/blocklyTwitter.js");

        public static ScriptInjectRequest FormsScriptRequest { get; } = ScriptInjectRequest.FromUrl(
            "blocklyForms", "/_content/Sitko.Blockly.Blazor/blocklyForms.js");
    }
}
