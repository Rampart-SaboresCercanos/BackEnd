using BackEnd.Orders.Application.Internal.CommandServices;
using BackEnd.Orders.Application.Internal.QueryServices;
using BackEnd.Orders.Domain.Repositories;
using BackEnd.Orders.Domain.Services;
using BackEnd.Orders.Infrastructure.Repositories;
using BackEnd.Shared.Domain.Repositories;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using BackEnd.Dishes;
using BackEnd.Dishes.Application.Internal.CommandServices; // Asegúrate de tener las referencias correctas
using BackEnd.Dishes.Application.Internal.QueryServices;
using BackEnd.Dishes.Domain.Services;
using BackEnd.Shared.Infrastructure;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
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
using BackEnd.Chefs.Infrastructure.Repositories;           // Agregado
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

/////////////////////////Begin Database Configuration/////////////////////////
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Verify Database Connection string
if (string.IsNullOrWhiteSpace(connectionString))
    throw new Exception("Database connection string is not set");

// Configure Database Context and Logging Levels
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    });
}
else if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
    {
        options.UseMySQL(connectionString)
            .LogTo(Console.WriteLine, LogLevel.Error)
            .EnableDetailedErrors();
    });
}

// Configure Dependency Injection for Orders
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderQueryService, OrderQueryService>();
builder.Services.AddScoped<IOrderCommandService, OrderCommandService>();

// Configure Dependency Injection for UserProfile
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserProfileQueryService, UserProfileQueryService>();
builder.Services.AddScoped<IUserProfileCommandService, UserProfileCommandService>();

// Configure Dependency Injection for Dishes
builder.Services.AddScoped<IDishRepository, DishRepository>(); // Asegúrate de que el repositorio esté implementado
builder.Services.AddScoped<IDishQueryService, DishQueryService>();
builder.Services.AddScoped<IDishCommandService, DishCommandService>();

// Configure Dependency Injection for Chefs (Agregado)
builder.Services.AddScoped<IChefRepository, ChefRepository>();
builder.Services.AddScoped<IChefQueryService, ChefQueryService>();
builder.Services.AddScoped<IChefCommandService, ChefCommandService>();

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

/*
// Configuración de Swagger solo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
*/

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rampart API V1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger esté disponible en la raíz
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
