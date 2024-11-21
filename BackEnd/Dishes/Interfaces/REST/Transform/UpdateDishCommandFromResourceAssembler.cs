using BackEnd.Dishes.Domain.Model.Commands;
using BackEnd.Dishes.Interfaces.REST.Resources;

namespace BackEnd.Dishes.Interfaces.REST.Transform;

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