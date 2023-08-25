using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using AutoMapper;
using CalifornianHealthMonolithic.Data;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Shared.Models.Identity;
using CalifornianHealthMonolithic.Shared.Services;
using CalifornianHealthMonolithic.WebApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

internal class Program
{
    [ExcludeFromCodeCoverage]
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigurationManager configuration = builder.Configuration;

        // Setup connection to database
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<CHDBContext>(options =>
            options.UseSqlServer(connectionString));

        // Setup Identity and JWT
        builder.Services.ConfigureJWT(configuration);
        builder.Services.ConfigureIdentity();

        builder.Services.ConfigureMapper();
        builder.Services.ConfigureCORS();

        // Inject AuthenticationService in order to generate JWT tokens
        builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
        builder.Services.AddScoped<ConfigurationManager>();


        // Add services to the container.
        builder.Services.AddRazorPages();

        builder.Services.ConfigureHttpClient();
        builder.Services.AddScoped<IAPIService, APIService>();

        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
        app.MapRazorPages();
        app.UseStatusCodePagesWithReExecute("/Error/{0}");

        app.Run();
    }
}