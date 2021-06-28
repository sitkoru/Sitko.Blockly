﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Sitko.Blockly.Blazor.Helpers
{
    public static class TwitchHelper
    {
        public static ValueTask RenderTwitchAsync(this IJSRuntime jsRuntime, ElementReference container,
            string? videoId,
            string? channelId, string? collectionId)
        {
            return jsRuntime.InvokeVoidAsync("Blockly.Twitch.render", container, videoId ?? "", channelId ?? "",
                collectionId ?? "");
        }

        public static ValueTask ClearTwitchAsync(this IJSRuntime jsRuntime, ElementReference container)
        {
            return jsRuntime.InvokeVoidAsync("Blockly.Twitch.clear", container);
        }
    }
}
