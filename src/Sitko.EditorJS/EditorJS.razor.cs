using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;

namespace Sitko.EditorJS;

public partial class EditorJS : ComponentBase, IAsyncDisposable
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private DotNetObjectReference<EditorJS>? instance;
    private bool rendered;
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;
    [Inject] protected IBlocksAccessor BlocksAccessor { get; set; } = null!;
    [Inject] protected IOptions<EditorJSOptions> EditorJSOptions { get; set; } = null!;
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;
    protected ElementReference EditorRef { get; set; }
    [Parameter] public EditorJSConfig? Config { get; set; }
    private string Data { get; set; } = "";

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

            var config = GetConfig();
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
            // if (!string.IsNullOrEmpty(OptionsProvider.Options.StylePath))
            // {
            //     injectRequests.Add(CssInjectRequest.FromUrl($"{OptionsProvider.Options.EditorClassName}Css",
            //         OptionsProvider.Options.StylePath));
            // }

            // foreach (var (key, path) in OptionsProvider.Options.GetAdditionalScripts(config))
            // {
            //     injectRequests.Add(ScriptInjectRequest.FromUrl(key, path));
            // }

            await ScriptInjector.InjectAsync(injectRequests, InitializeEditorAsync);
        }
    }

    private async Task InitializeEditorAsync(CancellationToken cancellationToken)
    {
        await JsRuntime.InvokeVoidAsync("window.SitkoEditorJS.init", cancellationToken, Id.ToString(),
            JsonSerializer.Serialize(GetConfig(), JsonOptions), instance);
        rendered = true;
    }

    private EditorJSConfig? GetConfig() => Config ?? BlocksAccessor.GetConfig(Id.ToString());


    protected ValueTask DestroyEditor()
    {
        rendered = false;
        return JsRuntime.InvokeVoidAsync("window.SitkoEditorJS.destroy", Id);
    }

    [JSInvokable]
    public Task OnSave(string data)
    {
        using var jDoc = JsonDocument.Parse(data);
        Data = JsonSerializer.Serialize(jDoc, new JsonSerializerOptions { WriteIndented = true });
        StateHasChanged();
        return Task.CompletedTask;
    }

    // private async ValueTask UpdateEditorAsync() =>
    //     await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.update", Id, EditorValue);
}

public record EditorJSConfig
{
    [JsonPropertyName("holder")] public required string Holder { get; init; }

    [JsonPropertyName("tools")] public Dictionary<string, EditorJSToolConfig> Tools { get; } = new();
}

public record EditorJSToolConfig
{
    [JsonPropertyName("className")] public required string ClassName { get; init; }
    [JsonPropertyName("config")] public required ContentBlockConfig Config { get; init; }
}
