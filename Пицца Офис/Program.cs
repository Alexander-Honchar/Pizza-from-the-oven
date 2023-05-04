using Microsoft.AspNetCore.Authentication.Cookies;
using Пицца_Офис.Models.Authorization;
using Пицца_Офис.Models.DTO;
using Пицца_Офис.Models.ViewModels;
using Пицца_Офис.Services;
using Пицца_Офис.Services.GenericServiceis;
using Пицца_Офис.Services.Metods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IMetods, Metods>();


builder.Services.AddHttpClient<IGenericServices<CategoryMenuDTO>, GenericServices<CategoryMenuDTO>>();
builder.Services.AddScoped<IGenericServices<CategoryMenuDTO>, GenericServices<CategoryMenuDTO>>();

builder.Services.AddHttpClient<IGenericServices<CategoryPizzaDTO>, GenericServices<CategoryPizzaDTO>>();
builder.Services.AddScoped<IGenericServices<CategoryPizzaDTO>, GenericServices<CategoryPizzaDTO>>();


builder.Services.AddHttpClient<IGenericServices<MenuItemDTO>, GenericServices<MenuItemDTO>>();
builder.Services.AddScoped<IGenericServices<MenuItemDTO>, GenericServices<MenuItemDTO>>();

builder.Services.AddHttpClient<IGenericServices<OrderViewModels>, GenericServices<OrderViewModels>>();
builder.Services.AddScoped<IGenericServices<OrderViewModels>, GenericServices<OrderViewModels>>();

builder.Services.AddHttpClient<IGenericServices<PizzaKingSizeDTO>, GenericServices<PizzaKingSizeDTO>>();
builder.Services.AddScoped<IGenericServices<PizzaKingSizeDTO>, GenericServices<PizzaKingSizeDTO>>();


builder.Services.AddHttpClient<IGenericServices<PizzaDTO>, GenericServices<PizzaDTO>>();
builder.Services.AddScoped<IGenericServices<PizzaDTO>, GenericServices<PizzaDTO>>();

builder.Services.AddHttpClient<IGenericServices<SizePizzaDTO>, GenericServices<SizePizzaDTO>>();
builder.Services.AddScoped<IGenericServices<SizePizzaDTO>, GenericServices<SizePizzaDTO>>();

builder.Services.AddHttpClient<IGenericServices<LoginRequestDTO>, GenericServices<LoginRequestDTO>>();
builder.Services.AddScoped<IGenericServices<LoginRequestDTO>, GenericServices<LoginRequestDTO>>();



builder.Services.AddHttpClient<IBaseServices, BaseServices>();
builder.Services.AddScoped<IBaseServices, BaseServices>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



// 2.Схема Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = "/Authentication/Login";
    options.LogoutPath = "/Authentication/Logout";
    options.AccessDeniedPath = "/Authentication/AccessDenied";
    options.SlidingExpiration = true;
});




// 1. Сессия для сохранения токена(ответа) в приложении
builder.Services.AddSession(options =>
{
    options.IOTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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

app.UseAuthentication(); // 2
app.UseAuthorization();

//Session
app.UseSession();  //1.Сессия для сохранения токена(ответа) в приложении

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
