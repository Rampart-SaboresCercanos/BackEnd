using BackEnd.Dishes.Domain.Model.Aggregates;
using BackEnd.Dishes.Interfaces.REST.Resources;

namespace BackEnd.Dishes.Interfaces.REST.Transform;

public class DishResourceFromEntityAssembler
{
    public static DishResource ToResourceFromEntity(Dish entity)
    {
        return new DishResource(
            entity.Id,
            entity.ChefId,
            entity.NameOfDish,
            entity.Ingredients,
            entity.PreparationSteps,
            entity.Favorite
        );
    }
}