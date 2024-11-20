using Backend.Dishes.Domain.Model.Commands;
using Backend.Dishes.Interfaces.REST.Resources;

namespace Backend.Dishes.Interfaces.REST.Transform;

public class UpdateDishCommandFromResourceAssembler
{
    public static UpdateDishCommand ToCommandFromResource(UpdateDishResource resource)
    {
        return new UpdateDishCommand(
            resource.Id,
            resource.ChefId,
            resource.NameOfDish,
            resource.Ingredients,
            resource.PreparationSteps,
            resource.Favorite
        );
    }
}