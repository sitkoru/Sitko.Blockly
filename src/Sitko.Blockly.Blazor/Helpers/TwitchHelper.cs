using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sitko.Blockly.Blazor.Helpers;

public static class TwitchHelper
{
    public static ValueTask RenderTwitchAsync(this IJSRuntime jsRuntime, ElementReference container,
        string? videoId,
        string? channelId, string? collectionId) =>
        jsRuntime.InvokeVoidAsync("Blockly.Twitch.render", container, videoId ?? "", channelId ?? "",
            collectionId ?? "");

    public static ValueTask ClearTwitchAsync(this IJSRuntime jsRuntime, ElementReference container) =>
        jsRuntime.InvokeVoidAsync("Blockly.Twitch.clear", container);
}
