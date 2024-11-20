namespace Backend.Dishes.Interfaces.REST.Resources;

public record DishResource(
    int Id,                          // Identificador del platillo
    int ChefId,                      // Identificador del chef
    string NameOfDish,              // Nombre del platillo
    List<string> Ingredients,        // Lista de ingredientes
    List<string> PreparationSteps,   // Pasos de preparación
    bool Favorite = false            // Default to false for a new dish
);