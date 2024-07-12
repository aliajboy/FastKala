using FastKala.Application.Data;
using FastKala.Application.Interfaces.Global;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.Interfaces.Product;
using FastKala.Application.Services.Global;
using FastKala.Application.Services.Order;
using FastKala.Application.Services.Products;
using FastKala.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlServer") ?? throw new InvalidOperationException("Connection string 'sqlserver' not found.");

builder.Services.AddDbContext<FastKalaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<FastKalaUser>(options =>
{
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 8;
}).AddErrorDescriber<PersianIdentityErrorDescriber>().AddEntityFrameworkStores<FastKalaContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(2);
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
// Services
builder.Services.AddTransient<IUploadService, UploadService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();

// Features
builder.Services.AddControllersWithViews();
builder.Services.AddResponseCompression();
//builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

//app.UseResponseCaching();

app.MapAreaControllerRoute(
    name: "adminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();
