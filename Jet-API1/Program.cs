using Jet_API1.Context;
using Jet_API1.Services.Implementations;
using Jet_API1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var con = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseInMemoryDatabase("Test");
});

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    // Default Lockout settings.
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;


    // password settings
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit= false;
    options.Password.RequiredLength= 6;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase= true;
    options.Password.RequireNonAlphanumeric= true;
    
    //user settings
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

    // signin settings
    options.SignIn.RequireConfirmedEmail = false;
});

builder.Services.AddAuthentication();
builder.Services.AddScoped<IPlaceService, PlaceService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IRegionService, RegionService>();
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddSwaggerGen();
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .CreateLogger();
builder.Host.UseSerilog();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name");
    });
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
