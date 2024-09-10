using System.Text.Json;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Sitko.Blazor.ScriptInjector;
using Sitko.EditorJS.Blocks;
using Sitko.EditorJS.Configuration;
using Sitko.EditorJS.Data;

namespace Sitko.EditorJS.Blazor;

public partial class EditorJS : InputBase<EditorJSData>, IAsyncDisposable
{
    private static readonly JsonSerializerOptions PrettyPrintJsonOptions = new() { WriteIndented = true };

    private DotNetObjectReference<EditorJS>? instance;
    [Inject] protected AntiforgeryStateProvider AntiForgery { get; set; } = null!;
    [Inject] protected IScriptInjector ScriptInjector { get; set; } = null!;
    [Inject] protected IBlocksAccessor BlocksAccessor { get; set; } = null!;
    [Inject] protected IOptions<EditorJSOptions> EditorJSOptions { get; set; } = null!;
    [Inject] protected IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter] public string? Config { get; set; }

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
                ScriptInjectRequest.FromUrl("SitkoEditorJS", "_content/Sitko.EditorJS.Blazor/EditorJS.razor.js",
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

    private async Task InitializeEditorAsync(CancellationToken cancellationToken) =>
        await ScriptInjector.InjectAsync(ScriptInjectRequest.Inline(Id.ToString(), $$"""
              window.SitkoEditorJS.antiForgeryToken = '{{AntiForgery.GetAntiforgeryToken()?.Value}}';
              window.SitkoEditorJS.configs['{{Id}}'] = {
                    holder: '{{Id}}',
                    tools: {
                        {{GetConfig()}}
                    }
              };
              """), async _ =>
        {
            await JsRuntime.InvokeVoidAsync("window.SitkoEditorJS.init", cancellationToken, Id.ToString(),
                instance, CurrentValue);
        }, cancellationToken);

    private string GetConfig() => Config ?? BlocksAccessor.GetConfig(Id);

    private ValueTask DestroyEditor() => JsRuntime.InvokeVoidAsync("window.SitkoEditorJS.destroy", Id);

    [JSInvokable]
    public Task OnSave(EditorJSData data)
    {
        CurrentValue = data;
        StateHasChanged();
        return Task.CompletedTask;
    }

    protected override bool TryParseValueFromString(string? value, out EditorJSData result,
        out string validationErrorMessage)
    {
        result = default!;
        validationErrorMessage = "";
        return false;
    }

    // private async ValueTask UpdateEditorAsync() =>
    //     await JsRuntime.InvokeVoidAsync("window.SitkoBlazorCKEditor.update", Id, EditorValue);
}
