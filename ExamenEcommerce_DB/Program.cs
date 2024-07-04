using ExamenEcommerce_DB.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Obtenemos la cadena de conexion
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

// Agregamos la configuracion para SQL
builder.Services.AddDbContext<ExamenEcommerceDbContext>(op => op.UseSqlServer(connectionString));

// Definimos la nueva politica de CORS
builder.Services.AddCors(op =>
{
    op.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activamos la nueva politica
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
