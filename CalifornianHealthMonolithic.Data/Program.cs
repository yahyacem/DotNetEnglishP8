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

// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();
// app.MapControllers();
// app.Run();
