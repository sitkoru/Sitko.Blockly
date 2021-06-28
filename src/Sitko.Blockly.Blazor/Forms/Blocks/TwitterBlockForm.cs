using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;
using Sitko.Core.App.Blazor.Forms;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class TwitterBlockForm<TForm> : BlockForm<TForm, TwitterBlock>
        where TForm : BaseForm, IBlocklyForm
    {
        protected ElementReference ContainerRef;
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
                await RenderTweetAsync();
            }
        }

        protected async Task RenderTweetAsync()
        {
            if (Block.TweetId != _lastRendered)
            {
                if (!string.IsNullOrEmpty(Block.TweetId))
                {
                    await JsRuntime.RenderTweetAsync(Block.TweetId, ContainerRef);
                }
                else
                {
                    await JsRuntime.ClearTweetAsync(ContainerRef);
                }

                _lastRendered = Block.TweetId;
            }
        }

        protected Task OnChangeAsync(string newUrl)
        {
            return RenderTweetAsync();
        }
    }
}
