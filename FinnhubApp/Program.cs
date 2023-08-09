using Entities;
using FinnhubApp.Models;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);


// Services 
builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddScoped<IStocksService, StocksService>();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<StockMarketDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlConnection"),
    builder => builder.MigrationsAssembly("FinnhubApp"));
});


var app = builder.Build();

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot");

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();

