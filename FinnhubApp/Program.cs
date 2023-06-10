using FinnhubApp.Models;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddSingleton<IStocksService, StocksService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();

