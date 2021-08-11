namespace Sitko.Blockly.Blazor.Helpers
{
    using Sitko.Blazor.ScriptInjector;

    public class JsHelper
    {
        // TODO: .NET 6 Preview 7 sdk can't pack multiple files as static web assets. Return to static web assets when .NET 6 is fixed.
        // TODO: Probably https://github.com/dotnet/sdk/pull/19482. Try in RC1.
        public static ScriptInjectRequest TwitchScriptRequest { get; } = ScriptInjectRequest.FromResource(
            "blocklyTwitch",
            typeof(JsHelper).Assembly,
            "blocklyTwitch.js");

        public static ScriptInjectRequest TwitterScriptRequest { get; } = ScriptInjectRequest.FromResource(
            "blocklyTwitter",
            typeof(JsHelper).Assembly,
            "blocklyTwitter.js");

        public static ScriptInjectRequest FormsScriptRequest { get; } = ScriptInjectRequest.FromResource(
            "blocklyForms", typeof(JsHelper).Assembly, "blocklyForms.js");
    }
}
