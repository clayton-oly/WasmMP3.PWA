using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WasmMp3.Client;
using WasmMp3.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//adicionado storage
builder.Services.AddScoped<StorageService>();

//DEVICE SERVICE
builder.Services.AddScoped<DeviceService>();


await builder.Build().RunAsync();
