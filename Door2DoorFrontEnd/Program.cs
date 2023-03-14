using Door2DoorLib.Factories;
using Door2DoorLib.Interfaces;
using Door2DoorLib.Managers;

var builder = WebApplication.CreateBuilder(args);

var build = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
IConfiguration config = build.Build();

//Initialize an instance of an IDatabase for manager injections
config["door2doordb"] = config.GetConnectionString("DefaultConnection");
var db = Door2DoorLib.Factories.DatabaseFactory.CreateDatabase(config, "door2doordb", DatabaseTypes.MySql);

//Manager dependency injections
builder.Services.AddScoped<IRouteManager, RouteManager>(manager => new RouteManager(db));
builder.Services.AddScoped<IAdminManager, AdminManager>(manager => new AdminManager(db));
builder.Services.AddScoped<ILocationManager, LocationManager>(manager => new LocationManager(db));
builder.Services.AddScoped<IDbLogManager, DbLogManager>(manager => new DbLogManager(db));

//Initialize the log that handles errors if database can not be reached
LogFactory.Initialize(Environment.CurrentDirectory + "\\TestLogs.txt");

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
