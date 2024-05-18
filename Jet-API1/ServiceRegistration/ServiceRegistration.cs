using Jet_API1.Context;
using Jet_API1.Helpers;
using Jet_API1.Identity;
using Jet_API1.Services.Implementations;
using Jet_API1.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace Jet_API1.ServiceRegistration;

public static class ServiceRegistration
{
    public static void Register(this IServiceCollection services, IConfiguration configuration)
    {
        //Swagger settings
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Ваше API",
                Version = "v1"
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                          Enter 'Bearer' [space] and then your token in the text input below.
                          \r\n\r\nExample: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });
        //SQL Connection
        var con = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(con);
        });

        //IdentityUser Settings
        services.AddIdentity<IdentityUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        //JWT Settings
        services .AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidIssuer = configuration["JWT:ValidIssuer"],

        ValidateAudience = false,
        ValidAudience = configuration["JWT:ValidAudience"],

        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
    };
});
        //AddPolicy
        services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityData.AdminPolicyName, p =>
            {
                p.RequireRole(IdentityData.AdminClaimName);
            });
            options.AddPolicy(IdentityData.SuperAdminPolicyName, p =>
            {
                p.RequireRole(IdentityData.SuperAdminClaimName);
            });
            options.AddPolicy("AdminOrSuperAdmin", policy =>
            {
                policy.RequireRole(IdentityData.AdminClaimName, IdentityData.SuperAdminClaimName);
            });

        });

        //Services
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
