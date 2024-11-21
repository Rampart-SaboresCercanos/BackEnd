namespace BackEnd.Dishes.Interfaces.REST.Resources;

public record CreateDishResource
(
    int ChefId,                      // Identificador del chef
    string NameOfDish,              // Nombre del platillo
    List<string> Ingredients,        // Lista de ingredientes
    List<string> PreparationSteps   // Pasos de preparación
);