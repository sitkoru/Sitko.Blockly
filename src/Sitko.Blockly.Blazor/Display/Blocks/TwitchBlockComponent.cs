using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;

namespace Sitko.Blockly.Blazor.Display.Blocks
{
    using Sitko.Blazor.ScriptInjector;

    public abstract class
        TwitchBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.TwitchBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
        private readonly ScriptInjectRequest twitchScriptRequest = ScriptInjectRequest.FromUrl(
            "twitchTwitter",
            "/_content/Sitko.Blockly.Blazor/twitch.js");

        protected ElementReference ContainerRef { get; set; }
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await ScriptInjector.InjectAsync(twitchScriptRequest,
                    async _ => await JsRuntime.RenderTwitchAsync(ContainerRef, Block.VideoId, Block.ChannelId,
                        Block.CollectionId));
            }
        }
    }
}
