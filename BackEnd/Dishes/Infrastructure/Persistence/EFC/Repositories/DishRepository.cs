using Backend.Dishes.Domain.Model.Aggregates;
using Backend.Dishes.Domain.Repositories;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Backend.Dishes.Infrastructure.Persistence.EFC.Repositories;

public class DishRepository(AppDbContext context): BaseRepository<Dish>(context), IDishRepository
{
    public async Task<IEnumerable<Dish>> FindByChefIdAsync(int chefId)
    {
        return await Context.Set<Dish>().Where(d => d.ChefId == chefId).ToListAsync();
    }

    public async Task<Dish?> FindByNameOfDishAndDishIdAsync(string nameOfDish, int dishId)
    {
        return await Context.Set<Dish>().FirstOrDefaultAsync(d => d.NameOfDish == nameOfDish && d.Id == dishId);
    }
}