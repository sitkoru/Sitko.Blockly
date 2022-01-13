using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;
using Sitko.Blockly.Blazor.Helpers;
using Sitko.Blockly.Blocks;

namespace Sitko.Blockly.Blazor.Forms.Blocks;

public abstract class
    TwitchBlockForm<TBlocklyFormOptions> : BlockForm<TwitchBlock, TBlocklyFormOptions>
    where TBlocklyFormOptions : BlocklyFormOptions
{
    private string? lastRendered;
    private bool rendered;
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
            await ScriptInjector.InjectAsync(JsHelper.TwitchScriptRequest, _ => RenderVideoAsync());
        }
    }

    protected async Task RenderVideoAsync()
    {
        if (Block.Url != lastRendered)
        {
            if (!string.IsNullOrEmpty(Block.Url))
            {
                await JsRuntime.RenderTwitchAsync(ContainerRef, Block.VideoId, Block.ChannelId, Block.CollectionId);
                rendered = true;
            }
            else
            {
                if (rendered)
                {
                    await JsRuntime.InvokeAsync<string>("Blockly.Twitch.clear", ContainerRef);
                    rendered = false;
                }
            }

            lastRendered = Block.Url;
        }
    }

    protected Task OnChangeAsync(string newUrl) => RenderVideoAsync();
}
