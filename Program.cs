using GCMS.Web.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using GCMS.Web.Utilities;
using GCMS.Web.Areas.Admin.Services.Interface;
using GCMS.Web.Areas.Admin.Services.Repository;
using GCMS.Web.Services.RabbitMQ;
using GCMS.Web.Areas.Admin.Services.RabbitMQ;
using GCMS.Web.Areas.Admin.Services.Email;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GCMSContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("GCMSDbContext")));
//Authentication Login Service
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(op => op.LoginPath = "/Account/Login");
//Add RabbitMQ:
builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddScoped<IRabbitMQService, RabbitMQService>();
//DI:
builder.Services.AddScoped<PersianDate>();
builder.Services.AddScoped<INewsCategoryRepository, NewsCategoryRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoloeRepository, RoleRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

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
