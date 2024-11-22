using System.Text.Json;
using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Orders.Domain.Model.Aggregates;
//using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Dishes.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using BackEnd.Posts.Domain.Model.Aggregates;

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

// Configura las columnas con sus tipos y restricciones
        builder.Entity<Chef>().Property(c => c.Name).HasColumnName("name")
            .IsRequired()
            .HasMaxLength(255); // Asegura que no se exceda el tamaño de un VARCHAR(255)
        builder.Entity<Chef>().Property(c => c.Gender).HasColumnName("gender")
            .IsRequired()
            .HasMaxLength(50); // Para mantener un tamaño razonable en la columna
        builder.Entity<Chef>().Property(c => c.Rating).HasColumnName("rating")
            .IsRequired();
        builder.Entity<Chef>().Property(c => c.IsFavorite).HasColumnName("favorite")
            .IsRequired(); // La propiedad Favorite es obligatoria (tipo booleano o tinyint(1))

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
        
        builder.UseSnakeCaseNamingConvention();
    }
}