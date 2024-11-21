using BackEnd.Orders.Application.Internal.CommandServices;
using BackEnd.Orders.Application.Internal.QueryServices;
using BackEnd.Orders.Domain.Repositories;
using BackEnd.Orders.Domain.Services;
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
using BackEnd.Dishes.Application.Internal.CommandService;
using BackEnd.Dishes.Application.Internal.QueryServices;
using BackEnd.Dishes.Domain.Repositories;
using BackEnd.Dishes.Domain.services;
using BackEnd.Dishes.Infrastructure.Persistence.EFC.Repositories;
using BackEnd.IAM.Application.ACL.Services;
using BackEnd.IAM.Application.Internal.CommandServices;
using BackEnd.IAM.Application.Internal.OutboundServices;
using BackEnd.IAM.Application.Internal.QueryServices;
using BackEnd.IAM.Domain.Repositories;
using BackEnd.IAM.Domain.Services;
using BackEnd.IAM.Infrastructure.Hashing.BCrypt.Services;
using BackEnd.IAM.Infrastructure.Persistence.EFC.Repositories;
using BackEnd.IAM.Infrastructure.Tokens.JWT.Configuration;
using BackEnd.IAM.Infrastructure.Tokens.JWT.Services;
using BackEnd.IAM.Interfaces.ACL;
using BackEnd.Orders.Infrastructure.Persistence.EFC.Repositories; // Agregado
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

if (connectionString is null)
{
    throw new InvalidOperationException("Connection string not found");
}

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
        
    options.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = "ACME.LearningCenterPlatform.API",
            Version = "v1",
            Description = "ACME Learning Center Platform API",
            TermsOfService = new Uri("https://acme-learning.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "ACME Studios",
                Email = "contact@acme.com"
            },
            License = new OpenApiLicense
            {
                Name = "Apache 2.0",
                Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
            }
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
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
//builder.Services.AddScoped<IChefRepository, ChefRepository>();
//builder.Services.AddScoped<IChefQueryService, ChefQueryService>();
//builder.Services.AddScoped<IChefCommandService, ChefCommandService>();

// Post Bounded Context
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IPostQueryService, PostQueryService>();
builder.Services.AddScoped<IPostCommandService, PostCommandService>();

//  IAM Bounded Context Injection Configuration

// TokenSettings Configuration

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();
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

// Apply CORS Policy
app.UseCors("AllowFrontend");
// Add Authorization Middleware to Pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
