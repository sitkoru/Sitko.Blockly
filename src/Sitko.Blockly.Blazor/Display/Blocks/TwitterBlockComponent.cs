using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;

namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class
        TwitterBlockComponent<TEntity> : BlockComponent<TEntity, Sitko.Blockly.Blocks.TwitterBlock>
    {
        protected ElementReference ContainerRef;
        [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await JsRuntime.RenderTweetAsync(Block.TweetId, ContainerRef);
            }
        }
    }
}
