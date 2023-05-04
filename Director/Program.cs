using Director.Models;
using Director.Models.Authorization;
using Director.Services;
using Director.Services.GenericServiceis;
using Director.Services.Metods;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<IMetods, Metods>();

builder.Services.AddHttpClient<IBaseServices, BaseServices>();
builder.Services.AddScoped<IBaseServices, BaseServices>();

builder.Services.AddHttpClient<IGenericServices<OrderViewForDirectorDTO>, GenericServices<OrderViewForDirectorDTO>>();
builder.Services.AddScoped<IGenericServices<OrderViewForDirectorDTO>, GenericServices<OrderViewForDirectorDTO>>();

builder.Services.AddHttpClient<IGenericServices<WorkerDTO>, GenericServices<WorkerDTO>>();
builder.Services.AddScoped<IGenericServices<WorkerDTO>, GenericServices<WorkerDTO>>();

builder.Services.AddHttpClient<IGenericServices<RolesDTO>, GenericServices<RolesDTO>>();
builder.Services.AddScoped<IGenericServices<RolesDTO>, GenericServices<RolesDTO>>();

builder.Services.AddHttpClient<IGenericServices<RegistrationRequestDTo>, GenericServices<RegistrationRequestDTo>>();
builder.Services.AddScoped<IGenericServices<RegistrationRequestDTo>, GenericServices<RegistrationRequestDTo>>();

builder.Services.AddHttpClient<IGenericServices<LoginRequestDTO>, GenericServices<LoginRequestDTO>>();
builder.Services.AddScoped<IGenericServices<LoginRequestDTO>, GenericServices<LoginRequestDTO>>();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// 2.Схема Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = "/Admin/Authentication/Login";
    options.LogoutPath = "/Admin/Authentication/Logout";
    options.AccessDeniedPath = "/Admin/Authentication/AccessDenied";
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
    app.UseExceptionHandler("/Error");
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

app.MapRazorPages();
app.MapControllers();

app.Run();
