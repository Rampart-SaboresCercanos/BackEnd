using System.Text.Json;
using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Orders.Domain.Model.Aggregates;
using BackEnd.Dishes.Domain.Model.Aggregates;
using BackEnd.IAM.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using BackEnd.Posts.Domain.Model.Aggregates;

using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Configuración para Post
        builder.Entity<Post>().ToTable("posts");
        builder.Entity<Post>().HasKey(up => up.id);
        builder.Entity<Post>().Property(up => up.dishId).IsRequired().HasColumnName("dishId");
        builder.Entity<Post>().Property(up => up.publishDate).IsRequired().HasColumnName("publishDate");
        builder.Entity<Post>().Property(up => up.stock).IsRequired().HasColumnName("stock");
        builder.Entity<Post>().Property(up => up.pricePerUnit).IsRequired().HasColumnName("pricePerUnit");

        // Configuración para Order
        builder.Entity<Order>().ToTable("order"); // Especifica el nombre de la tabla
        builder.Entity<Order>().HasKey(o => o.Id);
        builder.Entity<Order>().Property(o => o.customerId).IsRequired().HasColumnName("customer_id");
        builder.Entity<Order>().Property(o => o.orderDate).IsRequired().HasColumnName("order_date");
        builder.Entity<Order>().Property(o => o.deliveryDate).IsRequired().HasColumnName("delivery_date");
        builder.Entity<Order>().Property(o => o.deliveryTime).IsRequired().HasColumnName("delivery_time");
        builder.Entity<Order>().Property(o => o.paymentMethod).IsRequired().HasColumnName("payment_method");
        builder.Entity<Order>().Property(o => o.status).IsRequired().HasColumnName("status");

        // Configuración para Chef
        builder.Entity<Chef>().ToTable("chefs"); // Especifica el nombre de la tabla
        builder.Entity<Chef>().HasKey(c => c.Id); // Define la clave primaria
        builder.Entity<Chef>().Property(c => c.Id).IsRequired(); // La propiedad Id es obligatoria
        builder.Entity<Chef>().Property(c => c.Name).HasColumnName("name")
            .IsRequired(); // La propiedad Name es obligatoria
        builder.Entity<Chef>().Property(c => c.Rating).HasColumnName("rating")
            .IsRequired(); // La propiedad Rating es obligatoria
        builder.Entity<Chef>().Property(c => c.IsFavorite).HasColumnName("favorite")
            .IsRequired(); // La propiedad Favorite es obligatoria
        builder.Entity<Chef>().Property(c => c.Gender).HasColumnName("gender")
            .IsRequired(); // La propiedad Gender es obligatoria

        // Bounded Context Dish (definicion de las tablas)
        builder.Entity<Dish>().HasKey(f=> f.Id);
        builder.Entity<Dish>().Property(f=> f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Dish>().Property(f=> f.ChefId).IsRequired();
        builder.Entity<Dish>().Property(f=> f.NameOfDish).IsRequired();
        builder.Entity<Dish>().Property(f=> f.Favorite).IsRequired();   
        builder.Entity<Dish>().Property(f => f.Ingredients)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null))
            .IsRequired();

        builder.Entity<Dish>().Property(f => f.PreparationSteps)
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions)null))
            .IsRequired();
        builder.Entity<Dish>().Property(f=> f.CreatedDate).IsRequired();        
        builder.Entity<Dish>().Property(f=> f.UpdatedDate).IsRequired();
        
        // IAM Context
        builder.Entity<User>().HasKey(u => u.Id);
        builder.Entity<User>().Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        
        builder.UseSnakeCaseNamingConvention();
    }
}