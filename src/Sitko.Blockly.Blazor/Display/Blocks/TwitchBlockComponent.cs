using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;

namespace Sitko.Blockly.Blazor.Display.Blocks
{
    public abstract class TwitchBlockComponent<TEntity> : BlockComponent<TEntity, Sitko.Blockly.Blocks.TwitchBlock>
        where TEntity : IBlocklyEntity
    {
        protected ElementReference ContainerRef;
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                await JsRuntime.RenderTwitchAsync(ContainerRef, Block.VideoId, Block.ChannelId, Block.CollectionId);
            }
        }
    }
}
