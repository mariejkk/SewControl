using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SewControl.Web;
using SewControl.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<AuthHttpHandler>();

builder.Services.AddHttpClient("SewControlAPI", client =>
{
    client.BaseAddress = new Uri("https://localhost:7119/");
})
.AddHttpMessageHandler<AuthHttpHandler>();

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IHttpClientFactory>().CreateClient("SewControlAPI"));

builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<CostureraService>();
builder.Services.AddScoped<EncargoService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
