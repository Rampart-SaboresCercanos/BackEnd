using BackEnd.Dishes.Domain.Model.Commands;
using BackEnd.Dishes.Interfaces.REST.Resources;

namespace BackEnd.Dishes.Interfaces.REST.Transform;

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