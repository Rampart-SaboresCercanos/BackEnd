using BackEnd.Chefs.Domain.Model.Aggregates;
using BackEnd.Chefs.Domain.Repositories;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Configuration;
using BackEnd.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Chefs.Infrastructure.Repositories;

public class ChefRepository(AppDbContext context) : BaseRepository<Chef>(context), IChefRepository
{
    public async Task<IEnumerable<Chef>> FindAllAsync()
    {
        return await Context.Set<Chef>().ToListAsync();
    }

    public async Task<Chef?> FindByIdAsync(int chefId)
    {
        return await Context.Set<Chef>().FirstOrDefaultAsync(c => c.Id == chefId);
    }
    
    public async Task<IEnumerable<Chef>> FindByNameAsync(string name)
    {
        return await Context.Set<Chef>()
            .Where(c => c.Name.Contains(name))  // Ejemplo de búsqueda por nombre
            .ToListAsync();
    }

    public async Task<IEnumerable<Chef>> FindByRatingAsync(double rating)
    {
        return await Context.Set<Chef>()
            .Where(c => c.Rating >= rating)  // Ejemplo de búsqueda por rating
            .ToListAsync();
    }

    public async Task UpdateAsync(Chef chef)
    {
        Context.Set<Chef>().Update(chef);
        await Context.SaveChangesAsync();
    }

    public async Task RemoveAsync(Chef chef)
    {
        Context.Set<Chef>().Remove(chef);
        await Context.SaveChangesAsync();
    }
}
