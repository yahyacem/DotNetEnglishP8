using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace CalifornianHealthMonolithic.Shared.Services
{
    public static class ServiceExtensions
    {
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<Patient, AppRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;
                options.User.RequireUniqueEmail = false;

                // Sign in settings
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(AppRole), services);
            builder.AddEntityFrameworkStores<CHDBContext>().AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login";
            });
        }
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            // Set up JWT
            services.AddAuthentication(options =>
            {
                // Password settings.
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                // Configure token validation parameters, such as IssuerSigningKey, ValidIssuer, ValidAudience, etc.
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Configure token validation parameters as needed
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"], // Replace with your issuer
                    // ValidAudience = configuration["JwtSettings:Audience"], // Replace with your audience
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])) // Replace with your secret key
                };
            });
            services.AddAuthorization(options =>
                {
                    options.AddPolicy("AllowAll", policy =>
                    {
                        policy.RequireAssertion(context => true); // Always succeeds
                    });
            });
        }
        public static void ConfigureMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        public static void ConfigureHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient("APIConsultant", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5264");
            });
            services.AddHttpClient("APIBooking", client =>
            {
                client.BaseAddress = new Uri("http://localhost:5102");
            });
        }
        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
        }
    }
}