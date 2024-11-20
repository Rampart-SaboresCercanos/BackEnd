using BackEnd.Orders.Application.Internal.CommandServices;
using BackEnd.Orders.Application.Internal.QueryServices;
using BackEnd.Orders.Domain.Repositories;
using BackEnd.Orders.Domain.Services;
using BackEnd.Orders.Infrastructure.Repositories;
using BackEnd.Shared.Domain.Repositories;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using BackEnd.Shared.Infrastructure;
using BackEnd.UserProfile;
using BackEnd.UserProfile.Application.Internal.QueryServices;
using BackEnd.UserProfile.Application.Internal.CommandServices;
using BackEnd.UserProfile.Domain.Services;
using BackEnd.Posts.Application.Internal.CommandServices;
using BackEnd.Posts.Application.Internal.QueryServices;
using BackEnd.Posts.Domain.Repositories;
using BackEnd.Posts.Domain.Services;
using BackEnd.Posts.Infrastructure.Repositories;
using BackEnd.Chefs.Application.Internal.CommandServices; // Agregado
using BackEnd.Chefs.Application.Internal.QueryServices;   // Agregado
using BackEnd.Chefs.Domain.Repositories;                  // Agregado
using BackEnd.Chefs.Domain.Services;                     // Agregado
using BackEnd.Chefs.Infrastructure.Repositories;
using Backend.Dishes.Application.Internal.CommandService;
using Backend.Dishes.Application.Internal.QueryServices;
using Backend.Dishes.Domain.Repositories;
using Backend.Dishes.Domain.services;
using Backend.Dishes.Infrastructure.Persistence.EFC.Repositories; // Agregado
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//configure Lower Case URLs
builder.Services.AddRouting(options => options.LowercaseUrls = true);
// Configure Kebab Case Route Naming Convention
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/////////////////////////Begin Database Configuration/////////////////////////
// Add DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Verify Database Connection string
if (connectionString is null)
{
    throw new InvalidOperationException("Connection string not found");
}

// Configure Database Context and Logging Levels
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
    else
    {
        if (builder.Environment.IsProduction())
        {
            options.UseMySQL(connectionString)
                .LogTo(Console.WriteLine, LogLevel.Error);
        }
    }
});

// Configure Dependency Injection
// Bounded Context Injection Configuration for Business
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Order Bounded Context
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();

// UserProfile Bounded Context
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserProfileQueryService, UserProfileQueryService>();
builder.Services.AddScoped<IUserProfileCommandService, UserProfileCommandService>();

// Dish Bounded Context
builder.Services.AddScoped<IDishRepository, DishRepository>();
builder.Services.AddScoped<IDishQueryService, DishQueryService>();
builder.Services.AddScoped<IDishCommandService, DishCommandService>();

// Chef Bounded Context
builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<IChefQueryService, ChefQueryService>();
builder.Services.AddScoped<IChefCommandService, ChefCommandService>();

// Post Bounded Context
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();
builder.Services.AddScoped<IPostCommandService, PostCommandService>();

/////////////////////////End Database Configuration/////////////////////////
var app = builder.Build();

// Verify Database Objects are created
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Handle migrations
    context.Database.Migrate(); // Handle migrations
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
