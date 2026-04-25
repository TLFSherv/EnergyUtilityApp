using Microsoft.EntityFrameworkCore;
using EnergyUtilityApp;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<EnergyUtilityAppDbContext>( // register DbContext
    options => options.UseNpgsql(connString) // specify provider
);

// add identity services
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<EnergyUtilityAppDbContext>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<EnergyApiService>();
builder.Services.AddScoped<AppDbService>();
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
