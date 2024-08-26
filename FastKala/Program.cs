using FastKala.Application.Data;
using FastKala.Application.Interfaces.Global;
using FastKala.Application.Interfaces.OnlinePayment;
using FastKala.Application.Interfaces.Order;
using FastKala.Application.Interfaces.Product;
using FastKala.Application.Interfaces.Settings;
using FastKala.Application.Interfaces.ShopSettings;
using FastKala.Application.Services.Global;
using FastKala.Application.Services.OnlinePayment;
using FastKala.Application.Services.Order;
using FastKala.Application.Services.Products;
using FastKala.Application.Services.Settings;
using FastKala.Application.Services.ShopSettings;
using FastKala.Utilities;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddHttpClient("zarinpal", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://payment.zarinpal.com/");
    httpClient.Timeout = TimeSpan.FromSeconds(10);
});
builder.Services.AddHttpClient("tapin", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.tapin.ir/");
    httpClient.Timeout = TimeSpan.FromSeconds(5);
});

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddTransient<IUploadService, UploadService>();
builder.Services.AddSingleton<IPaymentService, PaymentService>();
builder.Services.AddSingleton<IZarinPalService, ZarinPalService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<ISiteSettings, SiteSettings>();
builder.Services.AddTransient<IShopSettings, ShopSettings>();

// Features
builder.Services.AddControllersWithViews();
builder.Services.AddWebOptimizer();
builder.Services.AddResponseCompression();

var app = builder.Build();

app.Use((context, next) =>
{
    context.Response.Headers.AltSvc = "h3=\":443\"";
    return next(context);
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseWebOptimizer();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseResponseCompression();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapAreaControllerRoute(
    name: "adminArea",
    areaName: "Admin",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();