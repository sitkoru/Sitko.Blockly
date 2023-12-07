using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sitko.Blockly.Demo.Client;
using Sitko.Blockly.Demo.Client.Data.Repositories;
using Sitko.Blockly.MudBlazorComponents;
using Sitko.Core.App.Localization;
using Sitko.Core.Blazor.MudBlazorComponents;
using Sitko.Core.Blazor.Wasm;
using Sitko.Core.Repository.Remote;
using Sitko.Core.Repository.Remote.Wasm;
using Sitko.Core.Storage.Remote;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder
    .AddSitkoCoreBlazorWasm()
    .AddMudBlazor()
    .AddMudBlazorBlockly(options =>
    {
        options.AddBlocks<MudBlazorBlocklyModule>();
    })
    .AddJsonLocalization(options => options.AddDefaultResource<Index>())
    .AddRemoteStorage<RemoteStorageOptions>((context, options) =>
    {
        context.Configuration.Bind("Storage:Remote:DigitClubRemoteStorageOptions");
    })
    .AddRemoteRepositories(options =>
    {
        options.AddRepository<PostRemoteRepository>();
    })
    .AddWasmHttpRepositoryTransport((context, options) =>
    {
        context.Configuration.Bind(context.IsDevelopment() ? "HttpRoutes:Development" : "HttpRoutes:Production");
    });


builder.ConfigureLocalization("ru-RU");

await builder.RunApplicationAsync();
