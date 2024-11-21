using Backend.Dishes.Domain.Model.Commands;
using Backend.Dishes.Interfaces.REST.Resources;

namespace Backend.Dishes.Interfaces.REST.Transform;

public class CreateDishCommandFromResourceAssembler
{
    public static CreateDishCommand ToCommandFromResource(CreateDishResource resource)
    {
        return new CreateDishCommand(
            resource.ChefId,
            resource.NameOfDish,
            resource.Ingredients,
            resource.PreparationSteps
        );
    }
}