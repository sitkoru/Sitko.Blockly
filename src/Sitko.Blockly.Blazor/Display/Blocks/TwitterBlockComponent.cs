using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;

namespace Sitko.Blockly.Blazor.Display.Blocks
{
    using Sitko.Blazor.ScriptInjector;

    public abstract class
        TwitterBlockComponent<TEntity, TListOptions> : BlockComponent<TEntity, Sitko.Blockly.Blocks.TwitterBlock,
            TListOptions> where TListOptions : BlazorBlocklyListOptions, new()
    {
        private readonly ScriptInjectRequest twitterScriptRequest = ScriptInjectRequest.FromUrl(
            "blocklyTwitter",
            "/_content/Sitko.Blockly.Blazor/twitter.js");

        protected ElementReference ContainerRef { get; set; }
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
        [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await ScriptInjector.InjectAsync(twitterScriptRequest,
                    async _ => await JsRuntime.RenderTweetAsync(Block.TweetId, ContainerRef));
            }
        }
    }
}
