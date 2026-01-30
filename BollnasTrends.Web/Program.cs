using Polly;
using Microsoft.EntityFrameworkCore;
using BollnasTrends.Infrastructure.Data;
using BollnasTrends.Core.Interfaces;
using BollnasTrends.Core.Services;
using BollnasTrends.Core.Strategies;
using BollnasTrends.Infrastructure.Repositories;

// Added Azure and started to implementing 
// Added a startup and deployment
var builder = WebApplication.CreateBuilder(args);

// Hämta connection string från inställningarna
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Registrera Databasen (Det här var raden som saknades!)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,  // Försök upp till 5 gånger
            maxRetryDelay: TimeSpan.FromSeconds(30),  // Vänta max 30 sek mellan retries
            errorNumbersToAdd: new[] { 40613 }  // Specifikt för ditt "not available"-fel
        );
    })
);

// 1. Koppla ihop Interfaces med Implementationer (Dependency Injection)
// "Om någon vill ha IPopulationRepository -> Ge dem ScbRepository"
builder.Services.AddHttpClient<IPopulationRepository, ScbRepository>()
    .AddTransientHttpErrorPolicy(policy => 
        policy.WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))));



// 2. Registrera dina strategier och context (så vi kan använda dem senare)
builder.Services.AddScoped<ITrendStrategy, SimpleGrowthStrategy>();
builder.Services.AddScoped<TrendContext>();


// Add services to the container.
// Tryng to locate the bugg in Azure
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