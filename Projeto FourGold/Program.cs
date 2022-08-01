using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projeto_FourGold.Areas.Identity.Data;
using Projeto_FourGold.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("FourGoldContextConnection") ?? throw new InvalidOperationException("Connection string 'FourGoldContextConnection' not found.");

builder.Services.AddDbContext<FourGoldContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<Cliente>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<FourGoldContext>();

builder.Services.AddScoped<IPixRepository, PixRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar injeção de dependência nos Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IPixRepository, PixRepository>();
builder.Services.AddScoped<IContaRepository, ContaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapRazorPages(); // Adicionado

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
