using CalifornianHealthMonolithic.Data;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared;
using Microsoft.EntityFrameworkCore;
using CalifornianHealthMonolithic.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Setup connection to database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<CHDBContext>(options =>
    options.UseSqlServer(connectionString));

// Setup Identity
builder.Services.ConfigureIdentity();

var app = builder.Build();