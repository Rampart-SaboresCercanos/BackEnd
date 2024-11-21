namespace Backend.Dishes.Domain.Model.Commands;

public record UpdateDishCommand(
    int Id,
    int ChefId,
    string NameOfDish,
    List<string> Ingredients,
    List<string> PreparationSteps,
    bool Favorite);
    