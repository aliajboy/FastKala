using FastKala.Application.Data;
using FastKala.Application.Interfaces.Global;
using FastKala.Application.Interfaces.Product;
using FastKala.Application.Services.Global;
using FastKala.Application.Services.Products;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("SqlServer") ?? throw new InvalidOperationException("Connection string 'FastKalaContextConnection' not found.");

builder.Services.AddDbContext<FastKalaContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<FastKalaUser>(options => options.SignIn.RequireConfirmedPhoneNumber = true).AddEntityFrameworkStores<FastKalaContext>();

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
// Services
builder.Services.AddTransient<IUploadService, UploadService>();
builder.Services.AddTransient<IProductService, ProductService>();

// Features
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddResponseCompression();
builder.Services.AddResponseCaching();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseResponseCompression();
app.UseResponseCaching();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");

app.Run();
