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
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<ILineaDeVentaServicio, LineaDeVentaServicio>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddScoped<IComprobanteServicio, ComprobanteServicio>();
builder.Services.AddScoped<IStockServicio, StockServicio>();
builder.Services.AddScoped<IServicioExternoServicio, ServicioExternoServicio>();
builder.Services.AddScoped<ICondicionTServicio, CondicionTributariaServicio>();
builder.Services.AddScoped<ITipoDeComprobante, TipoDeComprobanteServicio>();

builder.Services.AddScoped<IPagoEfectivoServicio, PagoEfectivoServicio>();
builder.Services.AddScoped<IPagoConTarjetaServicio, PagoConTarjetaServicio>();

builder.Services.AddScoped<IPagoServicio, PagoServicio>();



// A�ado MudBlazor
builder.Services.AddMudServices();


await builder.Build().RunAsync();
