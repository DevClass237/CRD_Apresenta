using Final.Components;
using PocheteAPI.Services;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PocheteDados.Data;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        "Server=localhost;Database=pochetesenai;User=root;Password=;Port=3306;",
        ServerVersion.AutoDetect("Server=localhost;Database=pochetesenai;User=root;Password=;Port=3306;")
    )
);

builder.Services.Configure<CircuitOptions>(options => {
    options.DetailedErrors = true;
});


builder.Services.AddHttpClient<AdminService>(client => {
    client.BaseAddress = new Uri("https://localhost:7123/");
});

builder.Services.AddHttpClient<UsuarioService>(client => {
    client.BaseAddress = new Uri("https://localhost:7123/");
});
builder.Services.AddHttpClient<ProfessorService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/");  // URL correta da sua API
});
builder.Services.AddHttpClient<MovimentacaoService>(client => {
    client.BaseAddress = new Uri("https://localhost:7123/");
});

builder.Services.AddHttpClient<CursoService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/"); // substitua pela sua URL base real da API
});

builder.Services.AddHttpClient<SalaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/");
});

builder.Services.AddHttpClient<PocheteService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/");
});

builder.Services.AddHttpClient<TurmaService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7123/");
});

builder.Services.AddServerSideBlazor()
    .AddCircuitOptions(options => {
        options.DetailedErrors = true;
    });

builder.Services.AddScoped<DadosService>();
builder.Services.AddScoped<UsuarioService>();


builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddAntiforgery();

// ✅ ADICIONE ESTA LINHA:
builder.Services.AddAuthorization();

builder.Services.AddMudServices();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization(); // Isso agora funcionará corretamente
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
