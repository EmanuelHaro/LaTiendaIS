using Microsoft.EntityFrameworkCore;
using LaTiendaIS.Shared.Models;
using LaTiendaIS.Utilidades;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


if(builder.Environment.EnvironmentName != "Testing")
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

//using (var scope = app.Services.CreateScope()) //Comentar para no hacer la migracion
//{
//    var dataContext = scope.ServiceProvider.GetRequiredService<DBLaTiendaContext>();
//    dataContext.Database.Migrate();
//}


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
