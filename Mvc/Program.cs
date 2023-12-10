using BL.Interface;
using BL.Services;
using DAL;
using DAL.DbModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Pepegov.UnitOfWork;
using Pepegov.UnitOfWork.EntityFramework.Configuration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<IWeatherForecastService, WeatherForecastService>();
builder.Services.AddMudServices();
builder.Services.AddDbContext<DefaultDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddUnitOfWork(x =>
{
    x.UsingEntityFramework((context, configurator) =>
    {
        configurator.DatabaseContext<DefaultDbContext>();
    });
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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
using (var scope = app.Services.CreateScope())
{
    await SD.Init(scope.ServiceProvider);
}
app.Run();