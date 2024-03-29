﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks;

public abstract class
    TwitterBlockForm<TBlocklyFormOptions> : BlockForm<TwitterBlock, TBlocklyFormOptions>
    where TBlocklyFormOptions : BlocklyFormOptions
{
    private string? lastRendered;
    protected ElementReference ContainerRef { get; set; }
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;
    protected virtual bool RenderOnInit => true;

    protected override FieldIdentifier CreateFieldIdentifier() => FieldIdentifier.Create(() => Block.Url);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender && RenderOnInit)
        {
            await ScriptInjector.InjectAsync(JsHelper.TwitterScriptRequest, _ => RenderTweetAsync());
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
