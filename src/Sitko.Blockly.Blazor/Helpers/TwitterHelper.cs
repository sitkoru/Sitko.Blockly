using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sitko.Blockly.Blazor.Helpers
{
    public static class TwitterHelper
    {
        public static ValueTask RenderTweetAsync(this IJSRuntime jsRuntime, string tweetId, ElementReference container)
        {
            return jsRuntime.InvokeVoidAsync("Blockly.Twitter.render", tweetId, container);
        }

        public static ValueTask ClearTweetAsync(this IJSRuntime jsRuntime, ElementReference container)
        {
            return jsRuntime.InvokeVoidAsync("Blockly.Twitter.clear", container);   
        }
    }
}
