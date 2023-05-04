using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Pizza_WebAPI.Data;
using Pizza_WebAPI.Mapping;
using Pizza_WebAPI.Models;
using Pizza_WebAPI.Repository.UnitOfWork;
using Pizza_WebAPI.Repository.WorkersRepositori;
using Pizza_WebAPI.Repository.AuthenticationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var stringConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(stringConnection));




////Identity
builder.Services.AddIdentity<Workers, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();





builder.Services.AddResponseCaching();  // Caching




//Authentication API
var key = builder.Configuration.GetValue<string>("ApiJWTToken:SecretKey");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    }); ;





// Add services to the container.
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IWorkersRepository, WorkersRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();




builder.Services.AddControllers(options =>
{
    options.CacheProfiles.Add("Default30",  // Caching
        new CacheProfile()
        {
            Duration = 30
        });
})
    .AddNewtonsoftJson(); //.AddNewtonsoftJson() - for usage jsonPatch(nuget AspNetCore.JsonPatch,)AspNetCore.Mvc.NewtonsoftJson




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//setting for Authentication API
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
            "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
            "Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});




builder.Services.AddAutoMapper(typeof(MappingConfigure));


#region Logger

// add logger "Sirilog"
Log.Logger = new LoggerConfiguration().MinimumLevel.Information()
                .WriteTo.File("logging/pizzaLog.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();

#endregion 

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
