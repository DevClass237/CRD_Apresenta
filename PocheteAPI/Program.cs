using Microsoft.AspNetCore.Antiforgery;
using Microsoft.EntityFrameworkCore;
using PocheteDados.Data;
using ArduinoAPIBridge;
using System.Text.Json.Serialization;
using PocheteAPI.Utilidades; // <-- Necess�rio para o conversor de DateTime

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("MariaDBConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("MariaDBConnection"))
    )
);

// CORS para permitir acesso do Blazor Server
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirBlazorServer", policy =>
    {
        policy.WithOrigins("https://localhost:7081")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Adicional para enviar cookies
    });
});

// Configura��o do controlador + JSON com hora local
builder.Services.AddControllers(options =>
{
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
})
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
    options.JsonSerializerOptions.Converters.Add(new JsonConverterDateTimeLocal());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.UseCors("PermitirBlazorServer");

app.UseAuthorization();

app.MapControllers();
var bridgeTask = Task.Run(() => BridgeArduino.Main());
app.Run();
/*app.Run("http://0.0.0.0:5288");*/
