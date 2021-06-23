using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public abstract class TwitchBlockFormBase : BaseBlockForm<TwitchBlock>
    {
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

        protected virtual bool RenderOnInit => true;

        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.ChannelId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender && RenderOnInit)
            {
                await JsRuntime.InvokeVoidAsync("Blockly.Twitch.load");
                await RenderVideoAsync();
            }
        }

        protected async Task RenderVideoAsync()
        {
            if (!string.IsNullOrEmpty(Block.Url))
            {
                var arg = new
                {
                    video = Block.VideoId,
                    channel = Block.ChannelId,
                    collection = Block.CollectionId,
                    id = Block.Id,
                    instance = DotNetObjectReference.Create(this)
                };
                await JsRuntime.InvokeAsync<string>(
                    "Blockly.Twitch.render",
                    arg);
            }
            else
            {
                var arg = new {id = Block.Id};
                await JsRuntime.InvokeAsync<string>("Blockly.Twitch.clear", arg);
            }
        }
    }
}
