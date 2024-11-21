using BackEnd.Dishes.Domain.Model.Aggregates;

namespace BackEnd.Dishes.Interfaces.ACL;

public interface IDishesContextFacade
{
    Task<int> CreateDish(Dish dish);
}