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
        private bool _rendered;
        private string? _lastRendered;
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

        protected virtual bool RenderOnInit => true;

        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.Url);
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
            if (Block.Url != _lastRendered)
            {
                if (!string.IsNullOrEmpty(Block.Url))
                {
                    await JsRuntime.RenderTwitchAsync(ContainerRef, Block.VideoId, Block.ChannelId, Block.CollectionId);
                    _rendered = true;
                }
                else
                {
                    if (_rendered)
                    {
                        await JsRuntime.InvokeAsync<string>("Blockly.Twitch.clear", ContainerRef);
                        _rendered = false;
                    }
                }

                _lastRendered = Block.Url;
            }
        }

        protected Task OnChangeAsync(string newUrl)
        {
            return RenderVideoAsync();
        }
    }
}
