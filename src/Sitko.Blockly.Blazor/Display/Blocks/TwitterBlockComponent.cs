using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    TwitterBlockComponent<TListOptions> : BlockComponent<TwitterBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
    protected ElementReference ContainerRef { get; set; }
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ScriptInjector.InjectAsync(JsHelper.TwitterScriptRequest,
                async _ => await JsRuntime.RenderTweetAsync(Block.TweetId, ContainerRef));
        }
    }
}
