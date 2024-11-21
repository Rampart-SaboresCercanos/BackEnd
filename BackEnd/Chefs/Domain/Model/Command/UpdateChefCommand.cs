namespace BackEnd.Chefs.Domain.Model.Command;

public record UpdateChefCommand(
    int Id,                       // ID del chef a actualizar
    string Name,                  // Nuevo nombre del chef
    string Gender,                // Nuevo género del chef
    double Rating,                 // Nuevo rating del chef
    bool IsFavorite              // Nuevo estado de favorito
);