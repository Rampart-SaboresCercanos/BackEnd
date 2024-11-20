using System.Text.Json;
using BackEnd.Orders.Domain.Model.Aggregates;
using BackEnd.UserProfile;
using BackEnd.Chefs.Domain.Model.Aggregates;
using Backend.Dishes.Domain.Model.Aggregates;
using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using BackEnd.Posts.Domain.Model.Aggregates;

namespace BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseSnakeCaseNamingConvention();

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
        builder.Entity<Order>().Property(o => o.paymentMethod).IsRequired().HasColumnName("payment_method");
        builder.Entity<Order>().Property(o => o.totalAmount).IsRequired().HasColumnName("total_amount");
        builder.Entity<Order>().Property(o => o.status).IsRequired().HasColumnName("status");
        builder.Entity<Order>().Property(o => o.detailsShown).IsRequired().HasColumnName("details_shown");

        // Configuración para Chef
        builder.Entity<Chef>().ToTable("chefs"); // Especifica el nombre de la tabla
        builder.Entity<Chef>().HasKey(c => c.Id); // Define la clave primaria
        builder.Entity<Chef>().Property(c => c.Id).IsRequired(); // La propiedad Id es obligatoria
        builder.Entity<Chef>().Property(c => c.Name).HasColumnName("name")
            .IsRequired(); // La propiedad Name es obligatoria
        builder.Entity<Chef>().Property(c => c.Rating).HasColumnName("rating")
            .IsRequired(); // La propiedad Rating es obligatoria
        builder.Entity<Chef>().Property(c => c.Favorite).HasColumnName("favorite")
            .IsRequired(); // La propiedad Favorite es obligatoria
        builder.Entity<Chef>().Property(c => c.Gender).HasColumnName("gender")
            .IsRequired(); // La propiedad Gender es obligatoria

        // Configurar PreferencesJson como la columna que almacenará los datos JSON de dishes
        builder.Entity<Order>()
            .Property(o => o.PreferencesJson)
            .IsRequired()
            .HasColumnName("preferences_json")
            .HasColumnType("TEXT"); // Asegura que el tipo sea TEXT o lo equivalente en tu base de datos

        // Ignorar dishes ya que es una propiedad de solo acceso en memoria que usa PreferencesJson
        builder.Entity<Order>().Ignore(o => o.dishes);

        // Configuración para UserProfile
        builder.Entity<ProfileData>().ToTable("user_profiles"); // Especifica el nombre de la tabla
        builder.Entity<ProfileData>().HasKey(up => up.Id);
        builder.Entity<ProfileData>().Property(up => up.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<ProfileData>().Property(up => up.Photo).IsRequired().HasColumnName("photo");
        builder.Entity<ProfileData>().Property(up => up.Name).IsRequired().HasColumnName("name");
        builder.Entity<ProfileData>().Property(up => up.Email).IsRequired().HasColumnName("email");
        builder.Entity<ProfileData>().Property(up => up.BirthDate).HasColumnName("birth_date").IsRequired();
        builder.Entity<ProfileData>().Property(up => up.Address).IsRequired().HasColumnName("address");
        builder.Entity<ProfileData>().Property(up => up.PaymentMethod).IsRequired().HasColumnName("paymentmethod");
        builder.Entity<ProfileData>().Property(up => up.CardNumber).HasColumnName("cardnumber").IsRequired();
        builder.Entity<ProfileData>().Property(up => up.YapeNumber).HasColumnName("yapenumber").IsRequired();
        builder.Entity<ProfileData>().Property(up => up.CashPayment).IsRequired().HasColumnName("cashpayment");

        // Ignorar la propiedad Preferences
        builder.Entity<ProfileData>().Ignore(up => up.Preferences);

        builder.Entity<ProfileData>()
            .Property(up => up.PreferencesJson)
            .HasColumnName("preferencesJson") // Asegúrate de que coincida con el nombre de la columna
            .HasColumnType("TEXT") // Tipo para almacenar JSON
            .IsRequired(false); // Puede ser opcional

        // Configuración para DishData
        builder.Entity<Dish>().HasKey(f => f.Id);
        builder.Entity<Dish>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Dish>().Property(f => f.ChefId).IsRequired();
        builder.Entity<Dish>().Property(f => f.NameOfDish).IsRequired();
        builder.Entity<Dish>().Property(f => f.Favorite).IsRequired();
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
    }
}