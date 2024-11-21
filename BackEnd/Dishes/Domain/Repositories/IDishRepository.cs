using Backend.Dishes.Domain.Model.Aggregates;
using BackEnd.Shared.Domain.Repositories;

namespace Backend.Dishes.Domain.Repositories;

public interface IDishRepository : IBaseRepository<Dish>
{
    Task<IEnumerable<Dish>> FindByChefIdAsync(int chefId);

    Task<Dish?> FindByNameOfDishAndDishIdAsync(string nameOfDish, int dishId);
}