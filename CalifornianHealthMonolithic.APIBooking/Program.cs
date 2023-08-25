using CalifornianHealthMonolithic.APIBooking.Repositories;
using CalifornianHealthMonolithic.APIBooking.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using CalifornianHealthMonolithic.Data;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Services;
using System.Diagnostics.CodeAnalysis;

internal class Program
{
    [ExcludeFromCodeCoverage]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigurationManager configuration = builder.Configuration;

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<CHDBContext>(options =>
            options.UseSqlServer(connectionString));

        // Add services to the container.
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        ValidIssuer = configuration["JwtSettings:Issuer"], // Replace with your issuer
                                                                           // ValidAudience = configuration["JwtSettings:Audience"], // Replace with your audience
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])) // Replace with your signing key
                    };
                });

        builder.Services.AddAuthorization(); // Add this line to enable authorization

        // Setup JWT
        // builder.Services.ConfigureJWT(configuration);
        builder.Services.ConfigureMapper();
        builder.Services.ConfigureCORS();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Add services and repositories
        builder.Services.AddScoped<IBookingRepository, BookingRepository>();
        builder.Services.AddScoped<IBookingService, BookingService>();
        builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}