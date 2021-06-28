using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class TwitchBlockForm<TForm> : BlockForm<TForm, TwitchBlock>
        where TForm : BaseForm, IBlocklyForm
    {
        protected ElementReference ContainerRef;
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
                await RenderVideoAsync();
            }
        }

        protected async Task RenderVideoAsync()
        {
            if (!string.IsNullOrEmpty(Block.Url))
            {
                await JsRuntime.RenderTwitchAsync(ContainerRef, Block.VideoId, Block.ChannelId, Block.CollectionId);
            }
            else
            {
                await JsRuntime.InvokeAsync<string>("Blockly.Twitch.clear", ContainerRef);
            }
        }
    }
}
