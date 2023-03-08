using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;

var builder = WebApplication.CreateBuilder(args);

var build = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
IConfiguration config = build.Build();

config["d2ddatabase-353031351df8"] = config.GetConnectionString("DefaultConnection");
var db = Door2DoorLib.Factories.DatabaseFactory.CreateDatabase(config, "d2ddatabase-353031351df8", DatabaseTypes.MySql);

builder.Services.AddScoped<IRouteManager, RouteManager>(manager => new RouteManager(db));
builder.Services.AddScoped<IAdminManager, AdminManager>(manager => new AdminManager(db));

LogFactory.Initialize(Environment.CurrentDirectory + "\\TestLogs.txt", db);

db.OpenConnectionAsync().Wait();

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
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
