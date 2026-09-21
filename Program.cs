using Microsoft.EntityFrameworkCore;
using TransportationManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TransportationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("TransportationConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Session must be before controllers use it
app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=login}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();