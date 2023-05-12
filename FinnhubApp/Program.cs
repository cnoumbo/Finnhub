using FinnhubApp.Models;
using FinnhubApp.Services;
using FinnhubApp.ServicesContract;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.Configure<TradingOptions>(
    builder.Configuration.GetSection("TradingOptions")
);
builder.Services.AddScoped<IFinnhubService, FinnhubService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

app.Run();

