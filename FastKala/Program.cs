using FastKala.Data;
using FastKala.Domain.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<FsContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddRazorPages();
builder.Services.AddResponseCompression(option => option.EnableForHttps = true);
builder.Services.AddResponseCaching();
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

app.UseAuthorization();

app.UseResponseCompression();
app.UseResponseCaching();

app.MapRazorPages();

app.Run();
