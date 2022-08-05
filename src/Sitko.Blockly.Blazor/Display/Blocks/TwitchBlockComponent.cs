using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Display.Blocks;

public abstract class
    TwitchBlockComponent<TListOptions> : BlockComponent<TwitchBlock,
        TListOptions> where TListOptions : BlazorBlocklyListOptions
{
    protected ElementReference ContainerRef { get; set; }
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await ScriptInjector.InjectAsync(JsHelper.TwitchScriptRequest,
                async _ => await JsRuntime.RenderTwitchAsync(ContainerRef, Block.VideoId, Block.ChannelId,
                    Block.CollectionId));
        }
    }
}
