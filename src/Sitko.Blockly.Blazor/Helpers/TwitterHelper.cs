using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sitko.Blockly.Blazor.Helpers;

public static class TwitterHelper
{
    public static ValueTask
        RenderTweetAsync(this IJSRuntime jsRuntime, string tweetId, ElementReference container) =>
        jsRuntime.InvokeVoidAsync("Blockly.Twitter.render", tweetId, container);

    public static ValueTask ClearTweetAsync(this IJSRuntime jsRuntime, ElementReference container) =>
        jsRuntime.InvokeVoidAsync("Blockly.Twitter.clear", container);
}
