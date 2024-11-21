namespace BackEnd.Dishes.Domain.Model.Commands;

public record CreateDishCommand(
    int ChefId,                      // ID del chef
    string NameOfDish,              // Nombre del platillo
    List<string> Ingredients,        // Lista de ingredientes
    List<string> PreparationSteps,   // Pasos de preparación
    bool Favorite = false            // Default to false for a new dish
);