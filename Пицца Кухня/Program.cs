using Microsoft.AspNetCore.Authentication.Cookies;
using �����_�����.Models.Authorization;
using �����_�����.Models.ViewModels;
using �����_�����.Services;
using �����_�����.Services.GenericServiceis;
using �����_�����.Services.Metods;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddScoped<IMetods, Metods>();

builder.Services.AddHttpClient<IBaseServices, BaseServices>();
builder.Services.AddScoped<IBaseServices, BaseServices>();

builder.Services.AddHttpClient<IGenericServices<OrderViewModels>, GenericServices<OrderViewModels>>();
builder.Services.AddScoped<IGenericServices<OrderViewModels>, GenericServices<OrderViewModels>>();

builder.Services.AddHttpClient<IGenericServices<LoginRequestDTO>, GenericServices<LoginRequestDTO>>();
builder.Services.AddScoped<IGenericServices<LoginRequestDTO>, GenericServices<LoginRequestDTO>>();



builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// 2.����� Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
    options.LoginPath = "/Authentication/Login";
    options.LogoutPath = "/Authentication/Logout";
    options.AccessDeniedPath = "/Authentication/AccessDenied";
    options.SlidingExpiration = true;
});




// 1. ������ ��� ���������� ������(������) � ����������
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
app.UseSession();  //1.������ ��� ���������� ������(������) � ����������

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
