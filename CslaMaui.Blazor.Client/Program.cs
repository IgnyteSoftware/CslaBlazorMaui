using Csla.Configuration;
using CslaMaui.Blazor.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddCsla(o => o
    .AddBlazorWebAssembly()
    .DataPortal(dpo => dpo
        .UseHttpProxy(options => options.DataPortalUrl = "https://localhost:7078/api/DataPortal")));

await builder.Build().RunAsync();
