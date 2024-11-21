namespace BackEnd.Chefs.Domain.Model.Command;

public record CreateChefCommand(
    string Name,                   // Nombre del chef
    string Gender,                 // Género del chef
    double Rating = 0.0,            // Rating del chef (por defecto es 0)
    bool IsFavorite = false       // Favorito (por defecto es false)

);