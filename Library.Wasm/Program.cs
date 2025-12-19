using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Library.Wasm;
using Library.Wasm.OpenApis;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<LibraryApiWrapper>((sp, client) =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var apiUrl = config.GetValue("Api:Url", "https://localhost:7037");
    client.BaseAddress = new Uri(apiUrl!);
});

builder.Services.AddBlazorise(options => { options.Immediate = true; })
    .AddBootstrap5Providers()
    .AddFontAwesomeIcons();

await builder.Build().RunAsync();
