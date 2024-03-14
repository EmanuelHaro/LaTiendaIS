using Microsoft.EntityFrameworkCore;
using LaTiendaIS.Shared.Models;
using LaTiendaIS.Utilidades;
using AutoMapper;

using LaTiendaIS.Repositorio.Contrato;
using LaTiendaIS.Repositorio.Implementacio;

using LaTiendaIS.ServiciosAPI.Implementacion;
using LaTiendaIS.ServiciosAPI.Contrato;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient(typeof(IGenericoRepositorio<>), typeof(GenericoRepositorio<>)); //averiguar
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IArticuloServicio, ArticuloServicio>();
builder.Services.AddScoped<IClienteServicio, ClienteServicio>();
builder.Services.AddScoped<IComprobanteServicio, ComprobanteServicio>();
builder.Services.AddScoped<ICondicionTServicio, CondicionTServicio>();
builder.Services.AddScoped<ILineaDeVentaServicio,LineaDeVentaServicio>();
builder.Services.AddScoped<IStockServicio, StockServicio>();
builder.Services.AddScoped<IVentaServicio, VentaServicio>();
builder.Services.AddScoped<ISExternoServicio, SExternoServicio>();
builder.Services.AddScoped<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddScoped<IColorArticuloServicio, ColorArticuloServicio>();
builder.Services.AddScoped<IMarcaServicio, MarcaServicio>();
builder.Services.AddScoped<ISucursalServicio, SucursalServicio>();
builder.Services.AddScoped<ITalleServicio, TalleServicio>();
builder.Services.AddScoped<ITipoDeComprobanteServicio, TipoDeComprobanteServicio>();

builder.Services.AddScoped<IPagoEfectivoServicio, PagoEfectivoServicio>();
builder.Services.AddScoped<IPagoConTarjetaServicio, PagoConTarjetaServicio>();

builder.Services.AddScoped<IPagoServicio, PagoServicio>();


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Services.AddDbContext<DBLaTiendaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"))
);
}



builder.Services.AddCors(opciones =>
{
    opciones.AddPolicy("nuevaPolitica", app =>
    {   
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddHttpClient();


var app = builder.Build();

using (var scope = app.Services.CreateScope()) //Comentar para no hacer la migracion
{
    var dataContext = scope.ServiceProvider.GetRequiredService<DBLaTiendaContext>();
    dataContext.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("nuevaPolitica");

app.UseAuthorization();


app.MapControllers();

app.Run();
public partial class Program { }
