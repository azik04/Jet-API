using Jet_API1.Context;
using Jet_API1.Helpers;
using Jet_API1.Services.Implementations;
using Jet_API1.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Jet_API1.ServiceRegistration;

public static class ServiceRegistration
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {


        var con = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(con);
        });

        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


        services.AddAuthorization(options =>
        {
            options.AddPolicy("Admin", policy =>
            {
                policy.RequireRole("Admin");
            });

            options.AddPolicy("User", policy =>
            {
                policy.RequireRole("User");
            });
        });

        services.AddScoped<IPlaceService, PlaceService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IRegionService, RegionService>();
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IFlightService, FlightService>();
        services.AddScoped<IVehicleService, VehicleService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<JwtHelper>();

        services.Configure<IdentityOptions>(options =>
        {
            // Default Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // Password settings
            options.Password.RequiredUniqueChars = 1;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;

            // User settings
            options.User.RequireUniqueEmail = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

            // Sign-in settings
            options.SignIn.RequireConfirmedEmail = false;
        });
    }
}
