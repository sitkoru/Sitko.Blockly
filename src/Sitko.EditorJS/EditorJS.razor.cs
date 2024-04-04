using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;
using Sitko.EditorJS.Blocks;
using Sitko.EditorJS.Blocks.Paragraph;
using Sitko.EditorJS.Configuration;
using Sitko.EditorJS.Data;

namespace Sitko.EditorJS;

public partial class EditorJS : ComponentBase, IAsyncDisposable
{
    private static readonly JsonSerializerOptions PrettyPrintJsonOptions = new() { WriteIndented = true };

    private DotNetObjectReference<EditorJS>? instance;
    private bool rendered;
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;
    [Inject] protected IBlocksAccessor BlocksAccessor { get; set; } = null!;
    [Inject] protected IOptions<EditorJSOptions> EditorJSOptions { get; set; } = null!;
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
    [Parameter] public EditorJSConfig? Config { get; set; }

    private EditorJSData? Data { get; set; } = new()
    {
        Time = DateTime.UtcNow.Ticks,
        Version = "somever",
        Blocks =
        [
            new ParagraphBlock
            {
                Id = Guid.NewGuid().ToString(), Data = new ParagraphBlockData { Text = "Мой клёвый текст" }
            }
        ]
    };

    public Guid Id { get; } = Guid.NewGuid();

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        instance?.Dispose();
        return DestroyEditor();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            instance = DotNetObjectReference.Create(this);

            var injectRequests = new List<InjectRequest>
            {
                ScriptInjectRequest.FromUrl("SitkoEditorJS", "_content/Sitko.EditorJS/EditorJS.razor.js",
                    InjectScope.Scoped),
                ScriptInjectRequest.FromUrl("editorjs", EditorJSOptions.Value.EditorJSScriptUrl, InjectScope.Scoped)
            };
            foreach (var (key, script) in BlocksAccessor.GetScripts())
            {
                injectRequests.Add(ScriptInjectRequest.FromUrl($"editorjs-{key}", script, InjectScope.Scoped));
            }

            await ScriptInjector.InjectAsync(injectRequests, InitializeEditorAsync);
        }
    }

    private async Task InitializeEditorAsync(CancellationToken cancellationToken)
    {
        await JsRuntime.InvokeVoidAsync("window.SitkoEditorJS.init", cancellationToken, Id.ToString(),
            GetConfig(), instance, Data);
        rendered = true;
    }

    private EditorJSConfig GetConfig() => Config ?? BlocksAccessor.GetConfig(Id.ToString());


    private ValueTask DestroyEditor()
    {
        rendered = false;
        return JsRuntime.InvokeVoidAsync("window.SitkoEditorJS.destroy", Id);
    }

    [JSInvokable]
    public Task OnSave(EditorJSData data)
    {
        Data = data;
        StateHasChanged();
        return Task.CompletedTask;
    }

    // private async ValueTask UpdateEditorAsync() =>
    //     await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.update", Id, EditorValue);
}

[PublicAPI]
public record EditorJSConfig
{
    [JsonPropertyName("holder")] public required string Holder { get; init; }

    [JsonPropertyName("tools")] public Dictionary<string, EditorJSToolConfig> Tools { get; } = new();
}

[PublicAPI]
public record EditorJSToolConfig
{
    [JsonPropertyName("className")] public required string ClassName { get; init; }
    [JsonPropertyName("config")] public required ContentBlockConfig Config { get; init; }
}
