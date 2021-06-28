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
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
        protected virtual bool RenderOnInit => true;

        protected override FieldIdentifier CreateFieldIdentifier()
        {
            return FieldIdentifier.Create(() => Block.TweetId);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender && RenderOnInit)
            {
                await RenderTweetAsync();
            }
        }

        protected ValueTask RenderTweetAsync()
        {
            return !string.IsNullOrEmpty(Block.TweetId)
                ? JsRuntime.RenderTweetAsync(Block.TweetId, ContainerRef)
                : JsRuntime.ClearTweetAsync(ContainerRef);
        }
    }
}
