using Sitko.Blockly.Demo;
using Sitko.Blockly.Demo.Client;
using Sitko.Blockly.Demo.Client.Pages;
using Sitko.Blockly.Demo.Components;
using Sitko.Blockly.Demo.Data;
using Sitko.Blockly.MudBlazorComponents;
using Sitko.Core.App;
using Sitko.Core.App.Localization;
using Sitko.Core.App.Web;
using Sitko.Core.Blazor.MudBlazor.Server;
using Sitko.Core.Blazor.Server;
using Sitko.Core.Db.Postgres;
using Sitko.Core.Repository.EntityFrameworkCore;
using Sitko.Core.Storage;
using Sitko.Core.Storage.FileSystem;
using Sitko.Core.Storage.Metadata.Postgres;
using Sitko.EditorJS.Image;
using Sitko.EditorJS.MudBlazor;

var builder = WebApplication.CreateBuilder(args);
builder
    .AddSitkoCoreBlazorServer()
    .AddMudBlazorServer()
    // .AddMudBlazorBlockly(options =>
    // {
    //     options.AddBlocks<MudBlazorBlocklyModule>();
    // })
    .AddInteractiveWebAssembly()
    // .AddJsonLocalization()
    .AddPostgresDatabase<BlocklyContext>()
    .AddEFRepositories<BlocklyContext>();

builder
    .AddSitkoCoreBlazorServer()
    .AddFileSystemStorage<BlocklyStorageOptions>()
    .AddPostgresStorageMetadata<BlocklyStorageOptions>();

builder.Services.AddEditorJSMudBlazor<StorageItem>();
// builder.Services.AddEditorJSBlocks();
builder.AddEditorJSImage<BlocklyStorageOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.ConfigureLocalization("ru-RU");

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.MapSitkoCoreBlazor<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Add).Assembly);

app.Run();
