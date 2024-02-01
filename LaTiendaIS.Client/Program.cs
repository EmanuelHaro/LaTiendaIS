using LaTiendaIS.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using LaTiendaIS.Client.Service.Contrato;
using LaTiendaIS.Client.Service.Implementacion;
using Blazored.Toast;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5108") });

builder.Services.AddScoped<IArticuloServicio, ArticuloServicio>();

// Añado MudBlazor
builder.Services.AddMudServices();


await builder.Build().RunAsync();
