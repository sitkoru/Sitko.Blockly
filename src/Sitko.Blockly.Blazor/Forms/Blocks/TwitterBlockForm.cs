﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks
{
    public abstract class
        TwitterBlockForm<TBlocklyFormOptions> : BlockForm<TwitterBlock, TBlocklyFormOptions>
        where TBlocklyFormOptions : BlocklyFormOptions
    {
        protected ElementReference ContainerRef { get; set; }
        private string? lastRendered;
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
        protected virtual bool RenderOnInit => true;

        protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.Url);

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
            if (Block.TweetId != lastRendered)
            {
                if (!string.IsNullOrEmpty(Block.TweetId))
                {
                    await JsRuntime.RenderTweetAsync(Block.TweetId, ContainerRef);
                }
                else
                {
                    await JsRuntime.ClearTweetAsync(ContainerRef);
                }

                lastRendered = Block.TweetId;
            }
        }

        protected Task OnChangeAsync(string newUrl) => RenderTweetAsync();
    }
}
