using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Services;
using BollnasTrends.Core.Strategies;
using BollnasTrends.Infrastructure.Repositories;

// Added Azure and started to implementing 
// Added a startup and deployment
var builder = WebApplication.CreateBuilder(args);

// 1. Koppla ihop Interfaces med Implementationer (Dependency Injection)
// "Om någon vill ha IPopulationRepository -> Ge dem ScbRepository"
builder.Services.AddScoped<IPopulationRepository, ScbRepository>();

// 2. Registrera dina strategier och context (så vi kan använda dem senare)
builder.Services.AddScoped<ITrendStrategy, SimpleGrowthStrategy>();
builder.Services.AddScoped<TrendContext>();
builder.Services.AddHttpClient();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();