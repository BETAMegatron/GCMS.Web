using GCMS.Web.Areas.Admin.Services.Email;
using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Areas.Admin.Services.RabbitMQ;
using GCMS.Web.Areas.Admin.Services.Repository;
using GCMS.Web.Data.Context;
using GCMS.Web.Services.Jwt;
using GCMS.Web.Services.JWT;
using GCMS.Web.Services.RabbitMQ;
using GCMS.Web.Services.Redis;
using GCMS.Web.Utilities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GCMSContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("GCMSDbContext")));
//Authentication Login Service
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op => op.LoginPath = "/Account/Login");
//Add RabbitMQ:
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
//EmailSenderService
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();
//RedisService:
builder.Services.Configure<RedisSetting>(builder.Configuration.GetSection("RedisSetting"));
builder.Services.AddSingleton<ConnectionMultiplexer>(sp =>
{
    var setting = sp.GetRequiredService<IOptions<RedisSetting>>().Value;
    var configuration = new ConfigurationOptions
    {
        EndPoints = { $"{setting.Host}:{setting.Port}" },
        Password = string.IsNullOrWhiteSpace(setting.Password) ? null : setting.Password
    };
    return ConnectionMultiplexer.Connect(configuration);
});
builder.Services.AddSingleton<IRedisService, RedisService>();
//JWT:
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<IJwtService, JwtService>();


//DI:
builder.Services.AddScoped<PersianDate>();
builder.Services.AddScoped<INewsCategoryRepository, NewsCategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoloeRepository, RoleRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<INewsRepository, NewsRepository>();

//--------------------------------------------------------------------------------------------------------------------------------------------------------
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Home}/{Action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
