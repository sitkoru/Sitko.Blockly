using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.AntDesignComponents.Forms.Blocks
{
    public abstract class TwitterBlockFormBase : BaseBlockForm<TwitterBlock>
    {
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

        protected async Task RenderTweetAsync()
        {
            if (!string.IsNullOrEmpty(Block.TweetId))
            {
                var arg = new {tweetId = Block.TweetId, id = Block.Id, instance = DotNetObjectReference.Create(this)};
                await JsRuntime.InvokeAsync<string>("Blockly.Twitter.render", arg);
            }
            else
            {
                var arg = new {id = Block.Id};
                await JsRuntime.InvokeAsync<string>("Blockly.Twitter.clear", arg);
            }
        }
    }
}
