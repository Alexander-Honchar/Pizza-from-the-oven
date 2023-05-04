using Microsoft.AspNetCore.Mvc;

using Serilog;
using Пицца_Сайт.Models.DTO;
using Пицца_Сайт.Models.ViewModels;
using Пицца_Сайт.Services;
using Пицца_Сайт.Services.GenericServiceis;
using Пицца_Сайт.Services.Metods;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IMetods, Metods>();


builder.Services.AddHttpClient<IGenericServices<MenuItemDTO>, GenericServices<MenuItemDTO>>();
builder.Services.AddScoped<IGenericServices<MenuItemDTO>, GenericServices<MenuItemDTO>>();

builder.Services.AddHttpClient<IGenericServices<PizzaDTO>, GenericServices<PizzaDTO>>();
builder.Services.AddScoped<IGenericServices<PizzaDTO>, GenericServices<PizzaDTO>>();

builder.Services.AddHttpClient<IGenericServices<OrderViewModels>, GenericServices<OrderViewModels>>();
builder.Services.AddScoped<IGenericServices<OrderViewModels>, GenericServices<OrderViewModels>>();



#region Session

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
});
#endregion




#region Logger

// add logger "Sirilog"
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.File("logging/pizzaLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();

#endregion 




var app = builder.Build();





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();



app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
