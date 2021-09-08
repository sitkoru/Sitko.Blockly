using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;

namespace Sitko.Blockly.Blazor.Display.Blocks
{
    using Sitko.Blazor.ScriptInjector;

    public abstract class
        TwitchBlockComponent<TListOptions> : BlockComponent<Sitko.Blockly.Blocks.TwitchBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
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
}
